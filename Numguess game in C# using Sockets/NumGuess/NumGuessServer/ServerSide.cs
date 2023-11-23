/////////////////////////////////////////////
//ServerSide.cs
//Sharry Saini and Joseph Limin
//November 18, 2023
/////////////////////////////////////////////
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;                                               //diagnostics for easier debugging
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;                                               //this one is self explanitory
using System.Text;
using System.Threading;                                                 //required for sending and recieving operations
using System.Windows.Forms;
using System.Xml.Linq;

namespace NumGuessServer
{
    public partial class ServerSide : Form
    {
        Socket listeningSocket = null;                                  // listening socket
        Socket otherSocket = null;                                      // socket that send/receive data from the user
        Thread readThread = null;                                       // thread that will read data from the user
        private const int GBufferSize = 1024;                           // buffer size of the reading array
        static Random _rand = new Random();                             // random use to generate random int 
        int correctNum = 0;                                             // coorect number that user will guess

        StatusLog log = null;

        public ServerSide()
        {
            InitializeComponent();
            log = new StatusLog("serverSideLog");
        }

        /// <summary>
        /// Server Load event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerSide_Load(object sender, EventArgs e)
        {
            Trace.WriteLine(TraceLineMessage("Server startup loaded..."));
            log.WriteLine(TraceLineMessage("Server startup loaded..."));

            TryConnecting(); // Try listening to the users
        }

        /// <summary>
        /// This function will initialize the listening, bind the socket, after the after the successfull 
        /// initiliaziation it will further invoke callBackAcceptDone() method that will close hte listening socket
        /// and will initialize our opertation socket
        /// </summary>
        private void TryConnecting()
        {
            // generating the current number to guess and displaying it on the UI
            correctNum = _rand.Next(1, 1001);
            UI_CorrectWords_lbl.Text = correctNum.ToString();

            // intializing the socket
            try
            {
                //DO NOT BE CREATIVE ABOUT THIS
                listeningSocket = new Socket(
                         AddressFamily.InterNetwork, // IP V4 address scheme
                         SocketType.Stream, // streaming socket (connection-based)
                         ProtocolType.Tcp); // TCP protocol :((
            }
            catch (Exception ex)
            {
                Trace.WriteLine(TraceLineMessage($"Failed to load {ex}"));
                log.WriteLine(TraceLineMessage($"Failed to load {ex}"));
            }

            // binding the socket to any IP and port 1666
            try
            {
                //points to any network device that can do the job
                listeningSocket.Bind(new IPEndPoint(IPAddress.Any, 1666));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(TraceLineMessage($"Failed to load {ex}"));
                log.WriteLine(TraceLineMessage($"Failed to load {ex}"));
            }

            // no clients to be put in the queue
            try
            {
                //how many clients can be in a queue
                listeningSocket.Listen(0);  //should it be one or 0?
            }
            catch (Exception ex)
            {
                Trace.WriteLine(TraceLineMessage($"Failed to listen. Error: {ex}"));
                log.WriteLine(TraceLineMessage($"Failed to listen. Error: {ex}"));
            }

            // finally calling the Accept method to initialize , operation socket
            try
            {
                listeningSocket.BeginAccept(callBackAcceptDone, null);      
            }
            catch (Exception ex)
            {
                Trace.WriteLine(TraceLineMessage($"Failed to bind. Error: {ex}"));
                log.WriteLine(TraceLineMessage($"Failed to bind. Error: {ex}"));
            }
        }

        /// <summary>
        /// This function will be invoket after success full initialization of listening socket
        /// </summary>
        /// <param name="result">async result containing properties of listner</param>
        private void callBackAcceptDone(IAsyncResult result)
        {
            try
            {
                //the second socket is to be defined by what the first socket throws out
                //since it throws out a socket, we can simply do a = opperations from the EndAccept
                //of the given result...
                otherSocket = listeningSocket.EndAccept(result);

                try
                {
                    if (listeningSocket != null)
                    {
                        // closing the listening socket and resetting it to null
                        listeningSocket.Close();
                        listeningSocket = null;
                    }
                    Invoke(new Action(ServerOpenStart));
                }
                catch (Exception exx)
                {
                    Trace.WriteLine(TraceLineMessage($"Unable to invoke ServerOpenStart(), Error: {exx}"));
                    log.WriteLine(TraceLineMessage($"Unable to invoke ServerOpenStart(), Error: {exx}"));
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(TraceLineMessage($"F in the chat.. Error: {ex}"));
                log.WriteLine(TraceLineMessage($"F in the chat.. Error: {ex}"));
            }
        }

        /// <summary>
        /// Function that will start read thread
        /// </summary>
        private void ServerOpenStart()
        {

            readThread = new Thread(callBackReadThread);
            readThread.IsBackground = true;
            readThread.Start();
        }

        /// <summary>
        /// Main thread function that will do function operations of reading and sending data
        /// </summary>
        private void callBackReadThread()
        {
            while (true) // forever thread
            {
                byte[] rxbytes = new byte[GBufferSize]; // intializing the byte array on each iteration

                try
                {
                    int RXBytesSize = otherSocket.Receive(rxbytes); // receiving data as bytes

                    if (RXBytesSize == 0) // if nothing received from the user 
                    {
                        //soft disconnect
                        try
                        {
                            Invoke(new Action(TryConnecting));
                        }
                        catch (Exception exx)
                        {
                            Trace.WriteLine(TraceLineMessage($"Unable to invoke TryConnecting(), Error: {exx}"));
                            log.WriteLine(TraceLineMessage($"Unable to invoke TryConnecting(), Error: {exx}"));
                        }
                        return;
                    }
                    //converting user response to string
                    string jsonString = Encoding.UTF8.GetString(rxbytes);

                    Trace.WriteLine(TraceLineMessage($"Received Successfull: Data Size: {RXBytesSize}, Data: {jsonString}\r\n"));
                    log.WriteLine(TraceLineMessage($"Received Successfull: Data Size: {RXBytesSize}, Data: {jsonString}\r\n"));

                    //deserializing the object
                    NumGuessData userGuess = JsonConvert.DeserializeObject<NumGuessData>(jsonString);


                    if (userGuess != null) // no need of checking this , but doing this for no reason(extra security layer on server LOLl..)
                    {
                        //will use this instance to send response back to the user
                        NumGuessData responseData = new NumGuessData();

                        if (userGuess.Data == correctNum) // if user response is correct
                        {
                            responseData.Data = 0;
                            correctNum = _rand.Next(1, 1001); // resetting the correct word
                            try
                            {
                                Invoke(new Action(() =>
                                {
                                    UI_CorrectWords_lbl.Text = correctNum.ToString(); // updating the UI
                                }));
                            }
                            catch (Exception exx)
                            {
                                Trace.WriteLine(TraceLineMessage($"Unable to invoke UI_CorrectWords_lbl.Text = correctNum.ToString( , Error: {exx}"));
                                log.WriteLine(TraceLineMessage($"Unable to invoke UI_CorrectWords_lbl.Text = correctNum.ToString( , Error: {exx}"));
                            }
                        }
                        else if (userGuess.Data < correctNum)// if user response is  too low
                        {
                            responseData.Data = -1;
                        }
                        else if (userGuess.Data > correctNum)// if user response is  too high
                        {
                            responseData.Data = 1;
                        }

                        //JSON serialize and Unicode encode
                        string frame = JsonConvert.SerializeObject(responseData);
                        byte[] buffer = Encoding.UTF8.GetBytes(frame);

                        //send response back to the user
                        otherSocket.Send(buffer, buffer.Count(), SocketFlags.None);
                    }
                    else // other wise try to listening again
                    {
                        try
                        {
                            Invoke(new Action(TryConnecting));
                        }
                        catch (Exception exx)
                        {
                            Trace.WriteLine(TraceLineMessage($"Unable to invoke TryConnecting(), Error: {exx}"));
                            log.WriteLine(TraceLineMessage($"Unable to invoke TryConnecting(), Error: {exx}"));
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    //hard disconnect
                    Trace.WriteLine(TraceLineMessage($"Error: {ex}"));
                    log.WriteLine(TraceLineMessage($"Error: {ex}"));
                    //reseting other socket again and Invoking the 
                    if (otherSocket != null)
                    {
                        try
                        {
                            otherSocket.Close();

                        }
                        catch (Exception exxx)
                        {
                            Trace.WriteLine(TraceLineMessage($"Unable to close the othersocket, Error: {exxx}"));
                            log.WriteLine(TraceLineMessage($"Unable to close the othersocket, Error: {exxx}"));
                        }

                        otherSocket = null;
                    }

                    // againg try to listen
                    try
                    {

                        Invoke(new Action(TryConnecting));
                    }
                    catch (Exception exx)
                    {
                        Trace.WriteLine(TraceLineMessage($"Unable to invoke TryConnecting(), Error: {exx}"));
                        log.WriteLine(TraceLineMessage($"Unable to invoke TryConnecting(), Error: {exx}"));
                    }
                }
            }
        }

        /// <summary>
        /// This function is use for proper Debugging message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string TraceLineMessage(string message)
        {
            return $"\r\nServer at [{DateTime.Now.ToString("HH:mm:ss")}] : {message}";
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
