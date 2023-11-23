/////////////////////////////////////////////
//ClientSide.cs
//Sharry Saini and Joseph Limin
//November 18, 2023
/////////////////////////////////////////////

using ICA06_ConnectLibrary;
using Sharry_DialogLibrary;

using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace NumGuess
{
    public partial class ClientSide : Form
    {
        const int GBufferSize = 1024;

        Socket clientSocket = null;
        Thread readThread = null;

        //UI    => might only be needed during UI reset - probably wont be defined class level - Jojo (Nov 19, 7:17PM)
        const int guessMax = 1000;
        const int guessMin = 1;
        const int guessCurrent = 500;

        StatusLog log = null;

        public ClientSide()
        {
            InitializeComponent();

            log = new StatusLog("clientSideLog");
        }

        private void Guess_Trackbar_Scroll(object sender, EventArgs e)
        {
            Current_Textbox.Text = Guess_Trackbar.Value.ToString(); 
        }

        private void Connect_Button_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Connect_Button_Click");
            log.WriteLine("Connect_Button_Click");

            Trace.WriteLine($"Connect_Button_Click() Is socket null? {clientSocket == null}");
            log.WriteLine($"Connect_Button_Click() Is socket null? {clientSocket == null}");

            Connect_Button.Enabled = false;
            Connect_Button.Text = "Connecting...";
            Connect_Button.ForeColor = Color.Yellow;

            UI_ConnectSharry_btn.Enabled = false;
            UI_ConnectSharry_btn.Text = "Connecting...";
            UI_ConnectSharry_btn.ForeColor = Color.Yellow;

            if (clientSocket == null)   //if not already connected: make a new connection
            {
                ConnectDialog dialog = new ConnectDialog("localhost", 1666, true, true);

                //check if dialog was closed properly by press of the "connect" button
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Trace.WriteLine("dialog.ShowDialog() == DialogResult.OK");
                    log.WriteLine("dialog.ShowDialog() == DialogResult.OK");

                    //adopt the public facing socket from the dialog
                    clientSocket = dialog.Socket;

                    Trace.WriteLine("testSocket = dialog.Socket");
                    log.WriteLine("testSocket = dialog.Socket");

                    //check if the socket is null: null if the connection was not made successfully
                    if (clientSocket != null)
                    {
                        StartGame();
                    }
                    else
                    {
                        Trace.WriteLine($"Socket is null. Public-facing socket not exposed.");
                        log.WriteLine($"Socket is null. Public-facing socket not exposed.");

                        ConnectButtonReset();
                    }
                }
                else
                {

                    Trace.WriteLine($"dialog.ShowDialog() == {dialog.DialogResult}");
                    log.WriteLine($"dialog.ShowDialog() == {dialog.DialogResult}");

                    ConnectButtonReset();
                }
            }
            else    //if already connected: the user probably wants to disconnect
            {
                Trace.WriteLine("Calling SocketDie() from Connect_Button_Click");
                log.WriteLine("Calling SocketDie() from Connect_Button_Click");

                SocketDie();
            }
        }

        private void StartGame()
        {
            Trace.WriteLine("testSocket is NOT null, keep going");
            log.WriteLine("testSocket is NOT null, keep going");

            Connect_Button.Enabled = true;
            Connect_Button.Text = "Connected";
            Connect_Button.ForeColor = Color.Green;

            UI_ConnectSharry_btn.Enabled = true;
            UI_ConnectSharry_btn.Text = "Connected";
            UI_ConnectSharry_btn.ForeColor = Color.Green;

            try
            {
                //start up the thread that will constantly await for data from the server
                Invoke(new Action(() => {
                    ReadThreadStart();
                    Guess_Button.Enabled = true;
                }));
                //ReadThreadStart();
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Exception occurred on Invoke(new Action(() => ReadThreadStart();));: {ex}");
                log.WriteLine($"Exception occurred on Invoke(new Action(() => ReadThreadStart();));: {ex}");

                ConnectButtonReset();
            }
        }
        private void ReadThreadStart()  //main thread
        {
            Trace.WriteLine("StartThread()");
            log.WriteLine("StartThread()");

            try
            {
                readThread = new Thread(callBackReadThread);
            }
            catch (Exception error)
            {
                Trace.WriteLine($"Error happened on readThread = new Thread(callBackReadThread): \r\n" + error.Message);
                log.WriteLine($"Error happened on readThread = new Thread(callBackReadThread): \r\n" + error.Message);
            }

            try
            {
                readThread.IsBackground = true;
            }
            catch (Exception error)
            {
                Trace.WriteLine($"Error happened on readThread.IsBackground = true: \r\n" + error.Message);
                log.WriteLine($"Error happened on readThread.IsBackground = true: \r\n" + error.Message);
            }

            try
            {
                readThread.Start();
            }
            catch (Exception error)
            {
                Trace.WriteLine($"Error happened on readThread.Start(): \r\n" + error.Message);
                log.WriteLine($"Error happened on readThread.Start(): \r\n" + error.Message);
            }

        }

        private void callBackReadThread()   //worker thread
        {
            Trace.WriteLine("cbRXThread()");
            log.WriteLine("cbRXThread()");

            Trace.WriteLine($"callBackReadThread() Is socket null? {clientSocket == null}");
            log.WriteLine($"callBackReadThread() Is socket null? {clientSocket == null}");

            // all the JSON content we have ever received
            // minus whatever has been processed as a coherent object
            // NOTE: this assumes that string encoing is not multibyte, or has no
            // boundaries!
            //string totalContent = "";

            while (true)
            {
                // rx just the bytes from this pass (could be a full frame or not)
                // make this buffer *really* small to test for quality fragment processing
                byte[] rxbytes = new byte[GBufferSize];

                try
                {
                    int RXBytesSize = clientSocket.Receive(rxbytes);

                    if (RXBytesSize == 0)
                    {
                        //soft disconnect
                        Trace.WriteLine($"Soft disconnect");
                        log.WriteLine($"Soft disconnect");
                        //code for resetting the game and allowing for a new connection
                        Invoke(new Action(SocketDie));
                        //SocketDie();

                        readThread = null;

                        return;
                    }

                    //data!
                    // only implemention of tracelIne message function
                    Trace.WriteLine(TraceLineMessage($"Received Successfull: Data Size: {RXBytesSize}, Data: {Encoding.UTF8.GetString(rxbytes)}\r\n"));
                    log.WriteLine(TraceLineMessage($"Received Successfull: Data Size: {RXBytesSize}, Data: {Encoding.UTF8.GetString(rxbytes)}\r\n"));

                    try
                    {
                        string readData = Encoding.UTF8.GetString(rxbytes);
                        NumGuessData result = JsonConvert.DeserializeObject<NumGuessData>(readData);

                        if (result.Data == 0)       //on the money
                        {
                            //Code for telling the user that he or she got it!
                            Invoke(new Action(() =>
                            {
                                MessageBox.Show($"You got it! The correct guess was {Current_Textbox.Text}. Starting up a new game...");
                                GuessReset();

                                Message_Textbox.Text = $"New game!";
                            }));


                            log.WriteLine("Win");
                        }
                        else if (result.Data > 0)   //too high
                        {
                            Invoke(new Action(() =>
                            {
                                Guess_Trackbar.Maximum = int.Parse(Current_Textbox.Text) - 1;
                                Guess_Trackbar.Value = Guess_Trackbar.Maximum;
                                Upper_Textbox.Text = Guess_Trackbar.Maximum.ToString();
                                Current_Textbox.Text = Guess_Trackbar.Value.ToString();
                                Guess_Trackbar.Enabled = true;
                                Guess_Button.Enabled = true;

                                Message_Textbox.Text = $"Too high!";
                            }));

                            log.WriteLine("Too high");
                        }
                        else if (result.Data < 0)   //too low
                        {
                            Invoke(new Action(() =>
                            {
                                Guess_Trackbar.Minimum = int.Parse(Current_Textbox.Text) + 1;
                                Guess_Trackbar.Value = Guess_Trackbar.Minimum;
                                Lower_Textbox.Text = Guess_Trackbar.Minimum.ToString();
                                Current_Textbox.Text = Guess_Trackbar.Value.ToString();
                                Guess_Trackbar.Enabled = true;
                                Guess_Button.Enabled = true;

                                Message_Textbox.Text = $"Too low!";
                            }));

                            log.WriteLine("Too low");
                        }

                        Guess_Trackbar.Enabled = true; //return control of the trackbar to the user
                    }
                    catch (Exception error)
                    {
                        Trace.WriteLine($"Error at Encoding.UTF8.GetString(rxbytes): {error}");
                        log.WriteLine($"Error at Encoding.UTF8.GetString(rxbytes): {error}");

                        MessageBox.Show("Unable to properly read data from the server. Please try guessing again.");
                    }
                }
                catch (Exception ex)
                {
                    //hard disconnect
                    Trace.WriteLine($"Hard disconnect");
                    log.WriteLine($"Hard disconnect");

                    //code to tell the user and reset the program
                    Invoke(new Action(SocketDie));
                    //SocketDie();
                    readThread = null;

                    return;
                }
            }
        }

        private void Guess_Button_Click(object sender, EventArgs e)
        {
            Guess_Trackbar.Enabled = false; //temporarily disable the changing of the trackbar by the user

            int guess = Guess_Trackbar.Value;

            //create instance of wire type, and populate with data
            NumGuessData guessData = new NumGuessData();
            guessData.Data = guess;

            //JSON serialize and Unicode encode
            string frame = JsonConvert.SerializeObject(guessData);
            byte[] buffer = Encoding.UTF8.GetBytes(frame);

            //send with socket to conencted target
            clientSocket.Send(buffer, buffer.Count(), SocketFlags.None);
        }

        private void SocketDie()    //main thread
        {
            Trace.WriteLine($"SocketDie() Is socket null? {clientSocket == null}");
            log.WriteLine($"SocketDie() Is socket null? {clientSocket == null}");

            if (clientSocket != null) 
            {

                Trace.WriteLine($"Socket is not null, killing the socket");
                log.WriteLine($"Socket is not null, killing the socket");

                try
                {
                    clientSocket.Shutdown(SocketShutdown.Both); //finish up sending or recieving on the socket
                }
                catch (Exception error)
                {
                    Trace.WriteLine($"Error occured on clientSocket.Shutdown(how): {error}");
                    log.WriteLine($"Error occured on clientSocket.Shutdown(how): {error}");
                }

                clientSocket.Close();

                clientSocket = null;

                Guess_Button.Enabled = false;

                ConnectButtonReset();
                GuessReset();
            }
            else
            {
                Trace.WriteLine($"Socket is already null, ignoring command");
                log.WriteLine($"Socket is already null, ignoring command");
            }
            
        }

        private void ConnectButtonReset()   //main thread
        {
            Connect_Button.Enabled = true;
            Connect_Button.Text = "Connect";
            Connect_Button.ForeColor = Color.Black;

            UI_ConnectSharry_btn.Enabled = true;
            UI_ConnectSharry_btn.Text = "Connect";
            UI_ConnectSharry_btn.ForeColor = Color.Black;
        }

        private void GuessReset()   //main thread
        {
            Guess_Trackbar.Maximum = guessMax;
            Guess_Trackbar.Minimum = guessMin;
            Guess_Trackbar.Value = guessCurrent;

            Upper_Textbox.Text = guessMax.ToString();
            Lower_Textbox.Text = guessMin.ToString();
            Current_Textbox.Text = guessCurrent.ToString();

            Guess_Trackbar.Enabled = true;
        }

        private void UI_ConnectSharry_btn_Click_1(object sender, EventArgs e)
        {    
            Trace.WriteLine("I_ConnectSharry_btn_Click_1");
            log.WriteLine("I_ConnectSharry_btn_Click_1");

            Trace.WriteLine($"Connect_Button_Click() Is socket null? {clientSocket == null}");
            log.WriteLine($"Connect_Button_Click() Is socket null? {clientSocket == null}");

            UI_ConnectSharry_btn.Enabled = false;
            UI_ConnectSharry_btn.Text = "Connecting...";
            UI_ConnectSharry_btn.ForeColor = Color.Yellow;

            Connect_Button.Enabled = false;
            Connect_Button.Text = "Connecting...";
            Connect_Button.ForeColor = Color.Yellow;

            if (clientSocket == null)   //if not already connected: make a new connection
            {
                ConnectionDialog dialog = new ConnectionDialog();


                //check if dialog was closed properly by press of the "connect" button
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Trace.WriteLine("dialog.ShowDialog() == DialogResult.OK");
                    log.WriteLine("dialog.ShowDialog() == DialogResult.OK");

                    //adopt the public facing socket from the dialog
                    clientSocket = dialog._sock;

                    Trace.WriteLine("testSocket = dialog.Socket");
                    log.WriteLine("testSocket = dialog.Socket");

                    //check if the socket is null: null if the connection was not made successfully
                    if (clientSocket != null)
                    {
                        StartGame();
                    }
                    else
                    {
                        Trace.WriteLine($"Socket is null. Public-facing socket not exposed.");
                        log.WriteLine($"Socket is null. Public-facing socket not exposed.");

                        ConnectButtonReset();
                    }
                }
                else
                {
                    Trace.WriteLine($"dialog.ShowDialog() == {dialog.DialogResult}");
                    log.WriteLine($"dialog.ShowDialog() == {dialog.DialogResult}");                

                    ConnectButtonReset();
                }
            }
            else    //if already connected: the user probably wants to disconnect
            {
                Trace.WriteLine("Calling SocketDie() from Connect_Button_Click");
                log.WriteLine("Calling SocketDie() from Connect_Button_Click");

                SocketDie();
            }
        }

        //Hey @JOJO ,I added this function to keep track of all the info properly , but I only implement this function on line 196(could be vary little bit)
        private string TraceLineMessage(string message)
        {
            return $"\r\nClient at [{DateTime.Now.ToString("HH:mm:ss")}] : {message}";
        }
    }


    //THE CLASS MUST BE DEFINED AFTER THE CLIENTSIDE CLASS IS DEFINED
    public class NumGuessData
    {
        public int Data { get; set; }
    }

    public class StatusLog
    {
        public string _FileName { get; private set; }
        private Queue<string> _qLogItems = new Queue<string>();
        private Thread _tLogThread = null;

        public StatusLog(string sFileName)
        {
            // attempt to open the log for writing, to ensure it's ok
            _FileName = sFileName;

            Console.WriteLine($"{_FileName}");

            // start the log writing thread
            try
            {
                _tLogThread = new Thread(ThreadLog);
                _tLogThread.IsBackground = true;
                _tLogThread.Start();

                // show startup log message
                WriteLine("----------------------------------------------------------------------");
            }
            catch (Exception)
            {
                //Console.WriteLine("Error starting log thread : " + err.Message);
            }
        }

        public void WriteLine(string sText)
        {
            string sLogItem = $"{DateTime.Now.ToShortDateString()} : {DateTime.Now.ToLongTimeString()} - {sText}";
            lock (_qLogItems)
                _qLogItems.Enqueue(sLogItem);
        }

        private void ThreadLog()
        {
            while (true)
            {
                Queue<string> tempQ = null;
                lock (_qLogItems)
                {
                    if (_qLogItems.Count > 0)
                    {
                        tempQ = new Queue<string>(_qLogItems);
                        _qLogItems.Clear();
                    }
                }

                if (tempQ != null)
                {
                    // burst log
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(_FileName, true, Encoding.UTF8))
                        {
                            while (tempQ.Count > 0)
                                sw.WriteLine(tempQ.Dequeue());
                            sw.Close();
                        }
                    }
                    catch (Exception err)
                    {
                        _FileName = "Error : " + err.Message;
                    }
                }

                Thread.Sleep(1);
            }
        }
    }
}
