using habbo.Kernel.Network;
using habbo.Kernel.Packets.com.sulake.habbo.communication.messages.outgoing.handshake;
//using habbo.Kernel.Packets.com.sulake.habbo.communication.messages.outgoing.handshake;
using habbo.Kernel.Packets.Interfaces;
using habbo.Kernel.Packets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Packets.com.sulake.habbo.communication.messages.incoming.handshake
{
    class GenerateSecretKeyMessageEvent : IMessageEvent
    {
        public void Invoke(Session Session, Messages.ClientPacket Packet)
        {
            string cipherPublickey = Packet.PopFixedString();

            if (!Session.HabboEncryption.InitializeRC4(cipherPublickey))
            {
                Console.WriteLine("Couldn't initialize " + this.GetType().Name + " with the publickey [" + cipherPublickey + "]");
                return;
            }

            SecretKeyComposer secret = new SecretKeyComposer(Session, Session.HabboEncryption.PublicKey.ToString());
        }
    }
}
