using habbo.Cryptography;
using habbo.Kernel.Network;
using habbo.Kernel.Packets.com.sulake.habbo.communication.messages.outgoing.handshake;
using habbo.Kernel.Packets.Interfaces;
using habbo.Kernel.Packets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Packets.com.sulake.habbo.communication.messages.incoming.handshake
{
    class InitCryptoMessageEvent : IMessageEvent
    {
        public void Invoke(Session Session, Messages.ClientPacket Packet)
        {
            Session.HabboEncryption = new Protocol(RsaKeys.N.GetKey, RsaKeys.E.GetKey, RsaKeys.D.GetKey);

            string token = new BigInteger(DiffieHellman.GenerateRandomHexString(15), 16).ToString();

            var primeGen = new List<string>();
            primeGen.Add(Session.HabboEncryption.Prime.ToString());
            primeGen.Add(Session.HabboEncryption.Generator.ToString());

            SystemApp.BannerTokens.Add(token, primeGen);

            InitCryptoMessageComposer init = new InitCryptoMessageComposer(Session, token);
        }
    }
}
