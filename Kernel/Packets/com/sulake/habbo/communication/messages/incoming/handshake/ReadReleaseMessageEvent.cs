using habbo.Kernel.Network;
using habbo.Kernel.Packets.Headers;
using habbo.Kernel.Packets.Interfaces;
using habbo.Kernel.Packets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Packets.com.sulake.habbo.communication.messages.incoming.handshake
{
    class ReadReleaseMessageEvent : IMessageEvent
    {
        public void Invoke(Session Session, Messages.ClientPacket Packet)
        {
            SystemApp.ConsoleSystem.PrintLine("[OUT][" + this.GetType().Name + "] >> " + Packet.PopFixedString());
        }
    }
}
