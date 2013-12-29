using habbo.Habbo.Characters;
using habbo.Kernel.Packets.Interfaces;
using habbo.Kernel.Packets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Packets.com.sulake.habbo.communication.messages.incoming.handshake
{
    class AuthenticationOKEvent : IMessageEvent
    {
        public void Invoke(Network.Session Session, Messages.ClientPacket Packet)
        {
            string SSOTicket = Packet.PopFixedString();

            Console.WriteLine("Received client_key > " + SSOTicket);

            Character Obj = SystemApp.CharacterManager.Authenticate(SSOTicket);

            if (Obj != null)
            {
                Session.UpdateCharacter(Obj);
                ServerPacket packet1 = new ServerPacket();
                packet1.AppendHeader(Headers.MessageComposerIds.AuthenticationOKMessageComposer);
                Session.Send(packet1);
                ServerPacket packet2 = new ServerPacket();
                packet2.AppendHeader(393);
                packet2.Append(10);
                packet2.Append("EXPERIMENTAL_TOOLBAR");
                packet2.Append("requirement.unfulfilled.beta_group_membership");
                packet2.Append(false);
                packet2.Append("EXPERIMENTAL_CHAT_BETA");
                packet2.Append("requirement.unfulfilled.beta_group_membership");
                packet2.Append(false);
                packet2.Append("USE_GUIDE_TOOL");
                packet2.Append("requirement.unfulfilled.helper_level_4");
                packet2.Append(false);
                packet2.Append("TRADE");
                packet2.Append("requirement.unfulfilled.citizenship_level_3");
                packet2.Append(false);
                packet2.Append("NEW_UI");
                packet2.Append(false);
                packet2.Append(false);
                packet2.Append(true);
                packet2.Append("HEIGHTMAP_EDITOR_BETA");
                packet2.Append("requirement.unfulfilled.feature_disabled");
                packet2.Append(false);
                packet2.Append("CALL_ON_HELPERS");
                packet2.Append(false);
                packet2.Append(false);
                packet2.Append(true);
                packet2.Append("VOTE_IN_COMPETITIONS");
                packet2.Append("requirement.unfulfilled.helper_level_2");
                packet2.Append(false);
                packet2.Append("JUDGE_CHAT_REVIEWS");
                packet2.Append("requirement.unfulfilled.helper_level_6");
                packet2.Append(false);
                packet2.Append("CITIZEN");
                packet2.Append("requirement.unfulfilled.citizenship_level_4");
                packet2.Append(false);
                Session.Send(packet2);
                
            }
            else
            {
                /*ServerPacket packet = new ServerPacket();
                packet.AppendHeader();
                packet.Append("Your SSO ticket was wrong. Please, try again.");
                packet.Append("");
                Session.Send(packet); not found yet*/ 
            }
        }
    }
}
