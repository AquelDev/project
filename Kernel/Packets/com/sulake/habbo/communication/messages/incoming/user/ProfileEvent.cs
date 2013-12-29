using habbo.Kernel.Packets.com.sulake.habbo.communication.messages.outgoing.user;
using habbo.Kernel.Packets.Interfaces;
using habbo.Kernel.Packets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Packets.com.sulake.habbo.communication.messages.incoming.user
{
    class ProfileEvent : IMessageEvent
    {
        public void Invoke(Network.Session Session, Messages.ClientPacket Packet)
        {
            new ShowProfileComposer(Session, Packet.PopFixedInt32());
        }
    }
}
