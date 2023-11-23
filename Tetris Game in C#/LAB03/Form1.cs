//***********************************************************************************
//Program:LAB03_SharrySingh
//Description:  In this lab we will create tetris game
//Date: 07 April,2023
//Author: Sharry Singh
//Course: CMPE2300
//Class: CNT.A01(Winter 2022)
//**********************************************************************************
using GDIDrawer;
using LAB03;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LAB03.Shape;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace LAB03
{
    public partial class Form1 : Form
    {
        const int  drWidth = 200;//width of drawer
        const int  drHeight = 600;//height of drawer
        const int subblockSize = 20;//size of sub block
        CDrawer _dr = null;//drawer object
        private Color[,] _playGrid = new Color[drWidth /subblockSize, drHeight / subblockSize];//grid of same size as drawer window
        Queue<Shape> _shapesQueue = new Queue<Shape>();//que to store shapes
        int CurrentLevel = 1;//initial level for the user
        int score = 0;//current score for the user
        Random _rnd = new Random();//random variable to genreate random shape
        bool isPause=true;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method will make all the grids as black initially
        /// </summary>
        private void MakeGrid()
        {
            //iterating through both x and y axis
            for (int x = 0; x < _playGrid.GetLength(0); x++)
            {
                for (int y = 0; y < _playGrid.GetLength(1); y++)
                {
                    _playGrid[x, y] = Color.Black;//setting back color of the GRID tiles
                }
            }
        }

        /// <summary>
        /// This method will fill the queue with different shapes
        /// Depending on the selected difficulty level
        /// Easy=>2 Shapes
        /// Medium=>4 Shapes
        /// Hard=>6 Shapes
        /// </summary>
        private void LoadLevel()
        {
            //initially we will fill the queue with 10 shapes
            //and next time we fill again shapes and increase the level and speed of timer
            for (int i = 0; i < 10; i++)
            {
                BlockType B=BlockType.TBlock;//block to be added

                /*****************************************/
                /*Selected difficulty by user*/
                if (UI_Easy_Rbn.Checked)
                    B = (BlockType)_rnd.Next(1, 3);//2 shapes
                if (UI_Medium_Rbn.Checked)
                    B = (BlockType)_rnd.Next(1, 5);//4 shapes
                if (UI_Hard_Rbn.Checked)
                    B = (BlockType)_rnd.Next(1, 7);//6 shapes
                /*****************************************/

                //addimg to queue with starting point at the center of drawer window
                _shapesQueue.Enqueue(new Shape(B, new Point(_playGrid.GetLength(0) / 2, 1)));

            }
        }
        /// <summary>
        /// This method will check for either the current shape go out of bound
        /// or collide with existing Shape
        /// </summary>
        /// <param name="points">List of projected points</param>
        /// <returns></returns>
        private bool DoesCollide(List<Point> points)
        {

            return points.TrueForAll(p =>
                points.Any(po => po.Y >= _playGrid.GetLength(1)) ||
                points.Any(po => po.X < 0 || po.X >= _playGrid.GetLength(0)) ||
                points.Any(po => _playGrid[po.X, po.Y] != Color.Black)
                );
        }

        /// <summary>
        /// This function will draw everything on the Drawer window and display stat
        /// </summary>
        private void DrawPlayArea()
        {
            //displaying border around grids
            for (int x = 0; x < _playGrid.GetLength(0); x++)
            {
                for (int y = 0; y < _playGrid.GetLength(1); y++)
                {
                    if (_playGrid[x, y] != Color.Black)
                        _dr.AddCenteredRectangle(x, y, 1, 1, _playGrid[x, y], 1, Color.White);
                }
            }
            //displaying next shape to the user
            if(_shapesQueue.Count>=2)
            {
                _dr.AddText($"Next Block  : {_shapesQueue.ElementAt(1).BType}", 15, _playGrid.GetLength(0) / 2, 1, 0, 0, Color.Yellow);

            }
            //alerting user with message that level is going to increase
            else
            {
                _dr.AddText($"!! Level increasing !!", 15, _playGrid.GetLength(0) / 2, 1, 0, 0, Color.Red);
            }
            //displaying current level to the user
            _dr.AddText($"Level : {CurrentLevel}", 15, _playGrid.GetLength(0) / 2, 2, 0, 0, Color.Yellow);
            //displaying score to the user
            _dr.AddText($"Score : {score}", 15, _playGrid.GetLength(0) / 2, 3, 0, 0, Color.Yellow);

        }
        /// <summary>
        /// This method will update score and grid if all horizontal lies are occupied properly
        /// </summary>
        private void UpdateScoreAndGrid()
        {
            // Iterate over all the rows from the bottom to the top
            for (int y = _playGrid.GetLength(1) - 1; y >= 0; y--)
            {
                bool rowIsFilled = true;

                // Checking if the current row is completely filled
                for (int x = 0; x < _playGrid.GetLength(0); x++)
                {
                    if (_playGrid[x, y] == Color.Black)
                    {
                        rowIsFilled = false;
                        break;
                    }
                }

                if (rowIsFilled)
                {
                    score += 15 * CurrentLevel;//incrementing score with 15 point times the level of the user

                    // Clear the row
                    for (int x = 0; x < _playGrid.GetLength(0); x++)
                    {
                        _playGrid[x, y] = Color.Black;
                    }

                    // Move all the sub-blocks above the cleared row down by one row
                    for (int j = y; j > 0; j--)
                    {
                        for (int x = 0; x < _playGrid.GetLength(0); x++)
                        {
                            _playGrid[x, j] = _playGrid[x, j - 1];
                        }
                    }

                    // Set the top row to black (since all the sub-blocks have moved down)
                    for (int x = 0; x < _playGrid.GetLength(0); x++)
                    {
                        _playGrid[x, 0] = Color.Black;
                    }

                    // Decrement y to check the current row again, since all the rows above it have moved down by one row
                    y++;
                }
            }

        }
        /// <summary>
        /// Method Check for the Game over
        /// </summary>
        /// <returns></returns>
        private bool CheckWin()
        {
            //checking if any grid at the location at starting point occupied by the color other than black
            //which mean grid has been fully filled
            for (int x = 0; x < _playGrid.GetLength(0); x++)
            {
                if (_playGrid[x, 1] != Color.Black)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// This event will beging the game 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_STart_Btn_Click(object sender, EventArgs e)
        {
            //closing previous drawer
            if (_dr != null)
            {
                _dr.Close();
            }
            CurrentLevel = 1;//reseting current level
            score = 0;//reseting the score
            MakeGrid();//making grid
            _shapesQueue.Clear();//clearing the queue
            LoadLevel();//loading the queue
           _dr = new CDrawer(drWidth, drHeight, false);//new drawer window with continous uodate false
           _dr.Scale = subblockSize;//drawer scale
            _dr.Position = new Point(800, 200);//position of drawer on user screen
            _dr.Clear();//clearing the drawer
            _dr.KeyboardEvent += _dr_KeyboardEvent;//keyboard event for moving the shape
            UI_STart_Btn.Enabled = false;//disabling the new game button
            UI_Timer.Interval = 200;//reseting time interval
            UI_Timer.Start();//starting timer 
            UI_Pause_btn.Enabled = true;

        }
        /// <summary>
        /// This event will generaate keyboard event to move ,rotate and fall the tetris block
        /// </summary>
        /// <param name="bIsDown"></param>
        /// <param name="keyCode"></param>
        /// <param name="dr"></param>
        private void _dr_KeyboardEvent(bool bIsDown, Keys keyCode, CDrawer dr)
        {
            if(_shapesQueue.Count>0)
            {
                //only when button is pressed
                if (bIsDown)
                {
                    /* In every button click , for  movement or rotation, we will apply the change, check with DoesCollide 
                       to see if the change is possible. If it is not, undo the change. This will stop rotation and movement 
                       outside the bounds of the play area, or illegal movement into dead sub-blocks
                    */

                    //moving the shape right
                    if (keyCode == Keys.Right)
                    {
                        Shape currentShape = _shapesQueue.Peek();

                        currentShape.MoveRight();

                        // Check if the shape collides with anything
                        if (DoesCollide(currentShape.GenPoints()))
                        {
                            // Undo the move
                            currentShape.MoveLeft();
                        }
                    }
                    //moving the shape left
                    if (keyCode == Keys.Left)
                    {
                        Shape currentShape = _shapesQueue.Peek();

                        currentShape.MoveLeft();

                        // Check if the shape collides with anything
                        if (DoesCollide(currentShape.GenPoints()))
                        {
                            // Undo the move
                            currentShape.MoveRight();
                        }
                    }
                    //rotating the shape clockwise
                    if (keyCode == Keys.Up)
                    {
                        Shape currentShape = _shapesQueue.Peek();

                        currentShape.RotInc();

                        // Check if the shape collides with anything
                        if (DoesCollide(currentShape.GenPoints()))
                        {
                            // Undo the move
                            currentShape.RotDec();
                        }
                    }
                    //rotating the shape anti-clockwise
                    if (keyCode == Keys.Down)
                    {
                        Shape currentShape = _shapesQueue.Peek();

                        currentShape.RotDec();

                        // Check if the shape collides with anything
                        if (DoesCollide(currentShape.GenPoints()))
                        {
                            // Undo the move
                            currentShape.RotInc();
                        }
                    }
                    //immediately fully drop the current block
                    if (keyCode == Keys.Space)
                    {
                        Shape currentShape = _shapesQueue.Peek();

                        // Check if the shape collides with anything
                        while (!DoesCollide(currentShape.GenPoints()))
                        {
                            // Undo the move
                            currentShape.Fall();
                            List<Point> ProjectedPoint = currentShape.ProjectPoints();

                            if (DoesCollide(ProjectedPoint))
                            {
                                List<Point> pointsToDraw = currentShape.GenPoints();
                                foreach (Point p in pointsToDraw)
                                {
                                    _playGrid[p.X, p.Y] = Color.DarkCyan;
                                }

                                // Load the next shape
                                _shapesQueue.Dequeue();
                                ++score;//incrementing score by one for one shape
                            }

                        }
                        
                    }
                }
            }  
        }

        /// <summary>
        /// This event will move the shape 1 unit in y-axis with checking tthe shape for any collision
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_Timer_Tick(object sender, EventArgs e)
        {
            if(_shapesQueue.Count>0)
            {            
                Shape  currentShape = _shapesQueue.Peek();//getting current shape
                   
                //checking for projected points
                List<Point> ProjectedPoint = currentShape.ProjectPoints();

                //checking if projected point cause any collision
                if (DoesCollide(ProjectedPoint) )
                {
                    List<Point> pointsToDraw = currentShape.GenPoints();//getting current point

                    //saving that point onto current play grid
                    foreach (Point p in pointsToDraw)
                    {
                        _playGrid[p.X, p.Y] = Color.DarkCyan;
                    }

                    _shapesQueue.Dequeue();//removing that shape
                    ++score;//incrementing by score per shape

                    //checking for the win
                    if (CheckWin())
                    {
                        UI_Timer.Enabled=false;//disabling the timer
                        _dr.Clear();//clear the drawer window

                        /*Displaying the message to the user*/
                        _dr.AddText("Game Over", 28, _playGrid.GetLength(0) / 2, (_playGrid.GetLength(1) / 2)-2, 0, 0, Color.Red);
                        _dr.AddText($"Your Level : {CurrentLevel}", 15, _playGrid.GetLength(0) / 2, (_playGrid.GetLength(1) / 2) , 0, 0, Color.Yellow);
                        _dr.AddText($"Your Score : {score}", 15, _playGrid.GetLength(0) / 2, (_playGrid.GetLength(1) / 2) + 1, 0, 0, Color.Yellow);
                        _dr.Render();

                        UI_STart_Btn.Enabled = true;//enabling the start button
                        UI_Pause_btn.Enabled = false;

                        return;
                    }
                }
                else
                {
                    currentShape.Fall();//falling shape by one y unit
                }

                UpdateScoreAndGrid();//checking for horizontal line filled for bonus
                _dr.Clear();//clearing the drawer
                DrawPlayArea();//displaying the grid area
                currentShape.Render(_dr);//displaying current shape
                _dr.Render();//rendering on the drawer                
                }
            

            //if queue is finished
            if (_shapesQueue.Count == 0)
            {
                LoadLevel();//loading shapes again
                if(UI_Timer.Interval > 50)
                {
                    UI_Timer.Interval -= 20;//incrementing speed
                    ++CurrentLevel;//incrementing current level
                }
            }
        }

        /// <summary>
        /// This event will pause /resume the game 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_Pause_btn_Click(object sender, EventArgs e)
        {
            if(isPause)
            {
                UI_Timer.Enabled = false;//disabling timer
                UI_Pause_btn.Text = "Resume";//changing text of button
                isPause = false;//clearing bool flag
            }
            else
            {
                UI_Timer.Enabled = true;//enabling the timer
                UI_Pause_btn.Text = "Pause";//changing the text of button
                isPause=true;//enabling the flag
            }
        }
    }
}


