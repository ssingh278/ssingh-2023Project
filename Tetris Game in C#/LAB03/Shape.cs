//***********************************************************************************
//Program:LAB03_SharrySingh
//Description:  Shape class
//Date: 07 April,2023
//Author: Sharry Singh
//Course: CMPE2300
//Class: CNT.A01(Winter 2022)
//**********************************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDIDrawer;
using static LAB03.Shape;

namespace LAB03
{
    internal class Shape
    {
        //all blocks of different shapes
        public enum BlockType
        {
            TBlock=1,
            Square,
            Line,
            ZBlock,
            JBlock,
            LBlock
        }

        public BlockType BType { get; private set; }//property for block
        public Point Pos { get; private set; }//current postion of block
        public Color Color { get; private set; }//current color of block
        public int Rot { get; private set; } = 0;//current rotation of block

        //dictonary to save the shapes with the points
        private static Dictionary<BlockType, Point[,]> _models =new Dictionary<BlockType, Point[,]>();

        /// <summary>
        /// Constructor that will accept the block type and posiiton
        /// </summary>
        /// <param name="bType"></param>
        /// <param name="pos"></param>
        public Shape(BlockType bType, Point pos)
        {
            BType = bType;
            Pos = pos;

            /* Different shape with different color*/
            if(BType==BlockType.TBlock)
                Color = Color.Red;
            if (BType == BlockType.Square)
                Color = Color.BlueViolet;
            if (BType == BlockType.Line)
                Color = Color.Green;
            if (BType == BlockType.ZBlock)
                Color = Color.Orange;
            if (BType == BlockType.JBlock)
                Color = Color.CornflowerBlue;
            if (BType == BlockType.LBlock)
                Color = Color.DarkSalmon;
        }

        /// <summary>
        /// Definition of all shapes
        /// </summary>
        static Shape()
        {
            _models[BlockType.TBlock] = new Point[4, 4]
            {
                 { new Point (0, 0), new Point (-1, 0), new Point (0, -1), new Point (1,0) },
                 { new Point (0, 0), new Point (0, -1), new Point (1, 0), new Point (0,1) },
                 { new Point (0, 0), new Point (-1, 0), new Point (1, 0), new Point (0,1) },
                 { new Point (0, 0), new Point (0, -1), new Point (-1, 0), new Point (0,1) }
            };
            _models[BlockType.Square] = new Point[4, 4]
            {
                    { new Point ( -1, -1), new Point (-1, 0), new Point (0, -1), new Point (0, 0) },
                    { new Point ( -1, -1), new Point (-1, 0), new Point (0, -1), new Point (0, 0) },
                    { new Point ( -1, -1), new Point (-1, 0), new Point (0, -1), new Point (0, 0) },
                    { new Point ( -1, -1), new Point (-1, 0), new Point (0, -1), new Point (0, 0) }

            };
            _models[BlockType.Line] = new Point[4, 4]
            {
                    { new Point ( -2, 0), new Point (-1, 0), new Point (0, 0), new Point (1, 0) },
                    { new Point ( 0, -2), new Point (0, -1), new Point (0, 0), new Point (0, 1) },
                    { new Point ( -2, 0), new Point (-1, 0), new Point (0, 0), new Point (1, 0) },
                    { new Point ( 0, -2), new Point (0, -1), new Point (0, 0), new Point (0, 1) }
            };
            _models[BlockType.ZBlock] = new Point[4, 4]
            {
                    { new Point ( 0, 0), new Point (1, 0), new Point (1, -1), new Point (2, -1) },
                    { new Point ( 0, 0), new Point (0, 1), new Point (1, 1), new Point (1, 2) },
                    { new Point ( 0, 0), new Point (1, 0), new Point (1, 1), new Point (2, 1) },
                    { new Point ( 1,-1), new Point (1,0), new Point (0,0), new Point (0, 1) }

            };
            _models[BlockType.JBlock] = new Point[4, 4]
            {
                    { new Point ( 0, 0), new Point (0,1), new Point (1, 1), new Point (2, 1) },
                    { new Point ( 1, 1), new Point (0, 1), new Point (0, 2), new Point (0, 3) },
                    { new Point ( 0, 1), new Point (1, 1), new Point (2, 1), new Point (2, 2) },
                    { new Point ( 0,1), new Point (1,1), new Point (1,0), new Point (1, -1) }

            };
            _models[BlockType.LBlock] = new Point[4, 4]
            {
                    { new Point ( 0, 0), new Point (1,0), new Point (2, 0), new Point (2, -1) },
                    { new Point ( 1, -2), new Point (1, -1), new Point (1, 0), new Point (2, 0) },
                    { new Point (0, 1),new Point ( 0, 0), new Point (1,0), new Point (2, 0) },
                    { new Point ( 0,0), new Point (1,0), new Point (1,1), new Point (1, 2) }
            };
        }

        /// <summary>
        /// Function that will genertate the point for current shape with offset of current location
        /// </summary>
        /// <returns>List of points</returns>
        public List<Point> GenPoints()
        {
            List<Point> Current = new List<Point>();
            for(int i = 0; i < 4;i++)
            {
                Point p = _models[BType][Rot, i];//getting current shape points
                Point newP = new Point(p.X + Pos.X, p.Y + Pos.Y);//adding the offset to show actual location
                Current.Add(newP);
            }
            return Current;
        }
        /// <summary>
        /// Function that will display the upcomming points
        /// </summary>
        /// <returns>list of the points</returns>
        public List<Point> ProjectPoints()
        {
            List<Point> points = GenPoints();//getting current points
            
            //incrementing the current point y axis
            List<Point> projectPoint=points.Select(p=>new Point(p.X,p.Y+1)).ToList();

            return projectPoint;
        }
        
        /// <summary>
        /// Method to render the current shape of drawer
        /// </summary>
        /// <param name="drawer">Cdrawer object</param>
        public void Render(CDrawer drawer)
        {
            List<Point> points = GenPoints();//current points

            foreach (Point point in points)
            {
                drawer.AddCenteredRectangle(point.X, point.Y, 1, 1,Color,1,Color.White);
            }
        }
        /// <summary>
        /// Incrementing y-axis by 1 to move shape dowm
        /// </summary>
        public void Fall()
        {
            Pos=new Point(Pos.X,Pos.Y+1);
        }
        /// <summary>
        /// Decrementing x-axis to move shape left
        /// </summary>
        public void MoveLeft()
        {          
            Pos = new Point( Pos.X-1, Pos.Y);
        }
        /// <summary>
        /// Incrementing x-axis to move shape left
        /// </summary>
        public void MoveRight()
        {
            Pos = new Point((Pos.X + 1), Pos.Y);
        }
        /// <summary>
        /// Rotating the current shape clockwise
        /// </summary>
        public void RotInc()
        {
            Rot = (Rot + 1) % 4;
        }
        /// <summary>
        /// Rotating the current shape anti-clockwise
        /// </summary>
        public void RotDec()
        {
            Rot = (((Rot - 1) + 4) % 4); 
        }
    }
}
