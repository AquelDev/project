using habbo.Kernel.Network;
using habbo.Kernel.Packets.Headers;
using habbo.Kernel.Packets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Packets.com.sulake.habbo.communication.messages.outgoing.handshake
{
    class InitCryptoMessageComposer : ServerPacket
    {
        public InitCryptoMessageComposer(Session Session, string token)
        {
            ServerPacket InitCryptoMessageComposer = new ServerPacket();
            InitCryptoMessageComposer.AppendHeader(MessageComposerIds.InitCryptoMessageComposer);
            InitCryptoMessageComposer.Append(token);
            InitCryptoMessageComposer.Append(token == "jj" ? true : false);
            Session.Send(InitCryptoMessageComposer);
        }
    }
}
