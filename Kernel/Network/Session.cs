using habbo.Cryptography;
using habbo.Habbo.Characters;
using habbo.Kernel.Packets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Network
{
    public class Session
    {
        public Socket Socket
        {
            get;
            set;
        }

        public Protocol HabboEncryption
        {
            get;
            set;
        }

        public SocketAsyncEventArgs ReceiveEventArgs
        {
            get;
            set;
        }

        public int SendBytesRemainingCount
        {
            get;
            set;
        }

        public Character Character
        {
            get;
            set;
        }

        public int BytesSentAlreadyCount
        {
            get;
            set;
        }

        public byte[] DataToSend
        {
            get;
            set;
        }

        public void OnConnectionClose()
        {
        }

        public void UpdateCharacter(Character Character)
        {
            this.Character = Character;
        }


        public void Send(string Data)
        {
            Socket.Send(Encoding.Default.GetBytes(Data));
        }

        public void Send(ServerPacket Packet)
        {
            Console.WriteLine(Packet.ToString());
            Socket.Send(Packet.ToByteArray());
        }

        public void FinishSend(IAsyncResult Result)
        {
            if (!Result.IsCompleted)
            {
                // > FAIL
            }
        }
    }
}
