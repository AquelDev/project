using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace habbo.Kernel.Packets.Messages
{
    public class ServerPacket
    {
        public List<byte> Message;
        public short headerId;

        public ServerPacket()
        {
            Message = new List<byte>();
        }

        public void AppendHeader(short headerId)
        {
            this.headerId = headerId;
            Short(headerId);
        }

        public void Append(object e)
        {
            if (e == null)
            {
                return;
            }

            if (e is int)
            {
                Integer((int)e);
            }
            else if (e is short)
            {
                Short((short)e);
            }
            else if (e is bool)
            {
                Boolean((bool)e);
            }
            else
            {
                UTF((string)e);
            }

        }

        public override string ToString()
        {
            return string.Concat(new object[] { "[", this.headerId, "] BODY: ", Encoding.Default.GetString(this.ToByteArray()).Replace(Convert.ToChar(0).ToString(), "[0]") });
        }

        public int headerid()
        {
            return headerId;
        }


        public void Integer(int Int32)
        {
            AddBytes(BitConverter.GetBytes(Int32), true);
        }

        public void Short(short Short)
        {
            AddBytes(BitConverter.GetBytes(Short), true);
        }

        public void UTF(string String)
        {
            Short((short)String.Length);
            AddBytes(Encoding.Default.GetBytes(String), false);
        }

        public void Boolean(bool Bool)
        {
            AddBytes(new byte[] { (Bool) ? (byte)1 : (byte)0 }, false);
        }

        public void AddBytes(byte[] Bytes, bool IsInt)
        {
            if (IsInt)
            {
                for (int i = Bytes.Length - 1; i > -1; i--)
                {
                    this.Message.Add(Bytes[i]);
                }
            }
            else
            {
                this.Message.AddRange(Bytes);
            }
        }

        public byte[] ToByteArray()
        {
            List<byte> NewMsg = new List<byte>();

            NewMsg.AddRange(BitConverter.GetBytes(Message.Count));
            NewMsg.Reverse();
            NewMsg.AddRange(Message);

            return NewMsg.ToArray();
        }
    }
}
