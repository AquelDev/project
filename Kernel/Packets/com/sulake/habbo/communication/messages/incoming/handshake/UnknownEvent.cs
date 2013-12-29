using habbo.Kernel.Packets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Packets.com.sulake.habbo.communication.messages.incoming.handshake
{
    class UnknownEvent : IMessageEvent
    {
        public void Invoke(Network.Session Session, Messages.ClientPacket Packet)
        {

        }
    }
}
