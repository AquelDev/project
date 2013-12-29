using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Network
{
    internal class MusListener : Socket
    {
        private static MusListener _instance;

        private readonly AsyncCallback _asyncAccept;

        public MusListener(IPEndPoint localendpoint)
            : base(localendpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
        {
            base.Bind(localendpoint);

            _asyncAccept = OnAccept;

            SystemApp.ConsoleSystem.PrintLine(string.Format("MUS Socket started on: {0}:{1}", localendpoint.Address,
                                                        localendpoint.Port));
        }

        private int ProcessId
        {
            get { return Process.GetCurrentProcess().Id; }
        }

        public static MusListener GetMusListener(int port = 0)
        {
            if (_instance == null)
            {
                _instance = new MusListener(new IPEndPoint(IPAddress.Any, port));
            }

            return _instance;
        }

        public void Start()
        {
            base.Listen(100);

            StartAccept();
        }

        private void StartAccept()
        {
            base.BeginAccept(_asyncAccept, this);
        }

        private void OnAccept(IAsyncResult IAs)
        {
            var socket = base.EndAccept(IAs);
            new MusConnection(socket.DuplicateAndClose(ProcessId));

            StartAccept();
        }
    }

    internal class MusConnection : Socket
    {
        private readonly AsyncCallback _asyncOnReceive;
        private readonly byte[] _buffer;

        public MusConnection(SocketInformation socketInformation)
            : base(socketInformation)
        {
            _buffer = new byte[512];

            _asyncOnReceive = OnReceive;

            WaitForData();
        }

        private void WaitForData()
        {
            if (!base.Connected)
            {
                return;
            }

            try
            {
                base.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, _asyncOnReceive, this);
            }
            catch
            {
                Destroy();
            }
        }

        private void OnReceive(IAsyncResult ias)
        {
            try
            {
                var length = base.EndReceive(ias);

                if (length < 6)
                {
                    throw new Exception("Connection closed");
                }

                var receivedBytes = new byte[length];
                Array.Copy(_buffer, receivedBytes, length);

                HandlePacket(receivedBytes);
            }
            catch
            {
                Destroy();
            }
            finally
            {
                Destroy();
            }
        }

        private void HandlePacket(byte[] receivedbytes)
        {
            string Data = Encoding.Default.GetString(receivedbytes);

            int header = int.Parse(Data.Split(Convert.ToChar(1))[0]);
            string param = Data.Split(Convert.ToChar(1))[1];

            SystemApp.ConsoleSystem.PrintLine(string.Format("Mus packet: ["+header+"] {0}", Data));

            switch (header)
            {
                case 0:
                    List<string> primeGen;

                    if (!SystemApp.BannerTokens.TryGetValue(param, out primeGen))
                    {
                        SystemApp.ConsoleSystem.PrintLine(string.Format("Unable to grab primegen from token: {0}", param));

                        break;
                    }

                    string outputString = primeGen[0] + ":" + primeGen[1];

                    base.Send(Encoding.Default.GetBytes(outputString));
                    break;

                default:
                    SystemApp.ConsoleSystem.PrintLine("Un listed packet");
                    break;
            }

            Destroy();
        }

        public void Destroy()
        {
            try
            {
                base.Shutdown(SocketShutdown.Both);
                base.Dispose();

                Array.Clear(_buffer, 0, _buffer.Length);

                GC.SuppressFinalize(this);
            }
            catch
            {
            }
        }
    }
}
