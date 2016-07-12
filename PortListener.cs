using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Windows.Forms;

namespace GeneiFurius
{
    public static class PortListener
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string message);

        public static NetworkStream staticStream;
        public static bool isConnected;

        public static void StartListening()
        {
            try
            {
                //AllocConsole();
                DataManagment data = new DataManagment();
                TcpListener r_Server = new TcpListener(IPAddress.Loopback, 1337);
                r_Server.Start();

                bool isActive = true;

                while (isActive)
                {
                    Console.Write("Waiting for a connection... ");

                    //!
                    //! Wait for the client to connect.
                    //!
                    TcpClient r_Client = r_Server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    //!
                    //! Get the stream.
                    //!
                    NetworkStream r_IOStream = r_Client.GetStream();
                    staticStream = r_IOStream;
                    string packetString = string.Empty;
                    //!
                    //! Keep executing while the client is active.
                    //!
                    while (r_Client.Connected)
                    {
                        using (var r_IOReader = new BinaryReader(r_IOStream))
                        {
                            isConnected = true;
                            DataManagment.ConsoleInterface("GeneiAO> DLL is Connected!");
                            while (r_IOReader != null)
                            {
                                if (r_IOStream.DataAvailable)
                                {
                                    Byte r_ID        = r_IOReader.ReadByte();
                                    UInt16 r_Length  = ReverseBytes(r_IOReader.ReadUInt16());
                                    byte[] r_Message = r_IOReader.ReadBytes(r_Length);

                                    if (r_ID == 0x02)
                                       packetString = data.AnalizeRecvPackets(Encoding.Unicode.GetString(r_Message));
                                    else if (r_ID == 0x03)
                                       packetString = data.AnalizeSendPackets(Encoding.Unicode.GetString(r_Message));

                                    //Console.WriteLine(Encoding.Unicode.GetString(r_Message));

                                    if(packetString != "")
                                        CreateLogs(packetString, r_ID);
                                }
                            }
                        }
                    }
                    
                    //!
                    //! Shutdown and end connection.
                    //!
                    r_Client.Close();
                    BasicForm.isConnected = false;
                    DataManagment.ConsoleInterface("GeneiAO> DLL is DisConnected!");
                    isActive = false;
                    Thread.CurrentThread.Abort();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("SocketException: {0}", exception);
            }
        }

        public static UInt16 ReverseBytes(UInt16 value)
        {
            return (UInt16)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
        }

        public static void SendToClient(String pMessage)
        {
            if (!string.IsNullOrEmpty(pMessage))
                Send(staticStream, pMessage, true);
            else
                DataManagment.ConsoleInterface("GeneiAO> Send was Null!!");

        }

        public static void SendToServer(String pMessage)
        {
            if (!string.IsNullOrEmpty(pMessage))
                Send(staticStream, pMessage, false);
            else
                DataManagment.ConsoleInterface("GeneiAO> Send was Null!!");
        }

        private static void Send(NetworkStream pStream, String pMessage, bool pForClient)
        {
            int len = (pMessage.Length + 1) * 2;

            Byte[] r_Reply = Enumerable.Repeat((byte)0x00, 0x03 + len).ToArray();

            r_Reply[0x00] = pForClient ? (byte)0x02 : (byte)0x03;
            r_Reply[0x01] = (Byte)((len >> 0x08) & 0xFF);
            r_Reply[0x02] = (Byte)(len & 0xFF);

            System.Array.Copy(Encoding.Unicode.GetBytes(pMessage), 0, r_Reply, 0x03, len - 0x02);
            pStream.Write(r_Reply, 0, r_Reply.Length);
            pStream.Flush();
        }

        public static void CreateLogs(string packet, byte id)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "//Logs";
            if (!Directory.Exists(path))
            { 
                Directory.CreateDirectory(path);            
            }

            if (id == 0x02)
            {
                using (StreamWriter sw = File.AppendText(path + "//Recv " + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".txt"))
                {
                    sw.WriteLine(packet);
                }
            }
            else if (id == 0x03)
            {
                using (StreamWriter sw = File.AppendText(path + "//Send " + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".txt"))
                {
                    sw.WriteLine(packet);
                }
            }   
        }

     
    }
}