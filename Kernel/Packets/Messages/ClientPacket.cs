using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using habbo.Kernel.Commons;


namespace habbo.Kernel.Packets.Messages
{
    public class ClientPacket
    {
        private byte[] Body;
        private int MessageId;
        private int Pointer;

        public short Header()
        {
            return (short) MessageId;
        }

        internal ClientPacket(int messageID, byte[] body)
        {
            this.Init(messageID, body);
        }

        internal void AdvancePointer(int i)
        {
            this.Pointer += i * 4;
        }

        private void CheckForExploits(string packetdata)
        {
        }

        internal void Init(int messageID, byte[] body)
        {
            if (body == null)
            {
                body = new byte[0];
            }
            this.MessageId = messageID;
            this.Body = body;
            this.Pointer = 0;
        }

        internal byte[] PlainReadBytes(int Bytes)
        {
            if (Bytes > this.RemainingLength)
            {
                Bytes = this.RemainingLength;
            }
            byte[] buffer = new byte[Bytes];
            int index = 0;
            for (int i = this.Pointer; index < Bytes; i++)
            {
                buffer[index] = this.Body[i];
                index++;
            }
            return buffer;
        }

        internal int PopFixedInt32()
        {
            int result = 0;
            int.TryParse(this.PopFixedString(Encoding.ASCII), out result);
            return result;
        }

        internal string PopFixedString()
        {
            return this.PopFixedString(Encoding.Default);
        }

        internal string PopFixedString(Encoding encoding)
        {
            return encoding.GetString(this.ReadFixedValue());
        }

        internal Boolean PopWiredBoolean()
        {
            if (this.RemainingLength > 0 && Body[Pointer++] == Convert.ToChar(1))
            {
                return true;
            }

            return false;
        }

        internal int PopWiredInt32()
        {
            if (this.RemainingLength < 1)
            {
                return 0;
            }
            int num = HabboEncoding.DecodeInt32(this.PlainReadBytes(4));
            this.Pointer += 4;
            return num;
        }

        internal uint PopWiredUInt()
        {
            return uint.Parse(this.PopWiredInt32().ToString());
        }

        internal byte[] ReadBytes(int Bytes)
        {
            if (Bytes > this.RemainingLength)
            {
                Bytes = this.RemainingLength;
            }
            byte[] buffer = new byte[Bytes];
            for (int i = 0; i < Bytes; i++)
            {
                int num2;
                this.Pointer = (num2 = this.Pointer) + 1;
                buffer[i] = this.Body[num2];
            }
            return buffer;
        }

        internal byte[] ReadFixedValue()
        {
            int bytes = HabboEncoding.DecodeInt16(this.ReadBytes(2));
            return this.ReadBytes(bytes);
        }

        public override string ToString()
        {
            return string.Concat(new object[] { "[", this.MessageId, "] BODY: ", Encoding.Default.GetString(this.Body).Replace(Convert.ToChar(0).ToString(), "[0]") });
        }

        internal int Id
        {
            get
            {
                return this.MessageId;
            }
        }

        internal int RemainingLength
        {
            get
            {
                return (this.Body.Length - this.Pointer);
            }
        }
    }
}
