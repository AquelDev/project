using habbo.Kernel.Packets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Packets.com.sulake.habbo.communication.messages.incoming.handshake
{
    class UNK : IMessageEvent
    {
        void IMessageEvent.Invoke(Network.Session Session, Messages.ClientPacket Packet)
        {

        }
    }
}
