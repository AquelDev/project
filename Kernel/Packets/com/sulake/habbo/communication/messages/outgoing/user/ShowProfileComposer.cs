using habbo.Habbo.Characters;
using habbo.Kernel.Network;
using habbo.Kernel.Packets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Packets.com.sulake.habbo.communication.messages.outgoing.user
{
    class ShowProfileComposer : ServerPacket
    {
        public ShowProfileComposer(Session Session, int userId)
        {
            Character character;

            if (SystemApp.CharacterManager.GetCharacter(userId, out character))
            {
                ServerPacket packet = new ServerPacket();
                packet.AppendHeader(3846);
                packet.Append(character.Id);
                packet.Append(character.Username);
                packet.Append(character.Figure);
                packet.Append(character.Motto);
                packet.Append("20-07-2013");
                packet.Append(10);
                packet.Append(11);
                packet.Append(false);
                packet.Append(false);
                packet.Append(true);
                packet.Append(0);
                //group?
                packet.Append(0);
                packet.Append(true);
                Session.Send(packet);
            }
        }
    }
}
