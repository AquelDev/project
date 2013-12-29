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
    class UserObjectEvent : IMessageEvent
    {
        public void Invoke(Network.Session Session, Messages.ClientPacket Packet)
        {
            ServerPacket packet = new ServerPacket();
            packet.AppendHeader(2717);
            packet.Append(Session.Character.Id); //userid
            packet.Append(Session.Character.Username); //username
            packet.Append(Session.Character.Figure); //look
            packet.Append("M"); //gender
            packet.Append(Session.Character.Motto); //motto
            packet.Append("Mikkel Friis"); //realname
            packet.Append(false);
            packet.Append(0);
            packet.Append(3);
            packet.Append(3);
            packet.Append(true);
            packet.Append("01-12-2012");
            packet.Append(string.Empty);
            Session.Send(packet);

        }
    }
}
