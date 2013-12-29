using habbo.Kernel.Packets.Interfaces;
using habbo.Kernel.Packets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Packets.com.sulake.habbo.communication.messages.incoming.games
{
    class GameListParser : IMessageEvent
    {
        public void Invoke(Network.Session Session, Messages.ClientPacket Packet)
        {
            ServerPacket packet = new ServerPacket();
            packet.AppendHeader(2389);
            packet.Append(1);

            packet.Append(3);
            packet.Append("basejump");
            packet.Append("68bbd2");
            packet.Append("");
            packet.Append("http://localhost/gordon/RELEASE63-201111301255-246708295/c_images/gamecenter_basejump/");
            packet.Append("");
            Session.Send(packet);
            ServerPacket packet2 = new ServerPacket();
            packet2.AppendHeader(1517);
            packet2.Append(1);
            packet2.Append(3);
            packet2.Append(5);
            packet2.Append(108);
            packet2.Append("BaseJumpMissile");
            packet2.Append(20);
            packet2.Append(109);
            packet2.Append("BaseJumpShield");
            packet2.Append(20);
            packet2.Append(110);
            packet2.Append("BaseJumpBigParachute");
            packet2.Append(20);
            packet2.Append(111);
            packet2.Append("BaseJumpDaysPlayed");
            packet2.Append(20);
            packet2.Append(107);
            packet2.Append("BaseJumpWins");
            packet2.Append(20);
            Session.Send(packet);
        }
    }
}
