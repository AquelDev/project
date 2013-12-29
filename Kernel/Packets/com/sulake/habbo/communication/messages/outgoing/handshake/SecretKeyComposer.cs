using habbo.Kernel.Network;
using habbo.Kernel.Packets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Packets.com.sulake.habbo.communication.messages.outgoing.handshake
{
    class SecretKeyComposer : ServerPacket
    {
        public SecretKeyComposer(Session Session, string publickey)
        {
            ServerPacket SecretKeyComposer = new ServerPacket();
            SecretKeyComposer.AppendHeader(Headers.MessageComposerIds.SecretKeyComposer);
            SecretKeyComposer.Append(publickey);
            Session.Send(SecretKeyComposer);
        }
    }
}
