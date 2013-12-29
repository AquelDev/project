using habbo.Kernel.Network;
using habbo.Kernel.Packets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Packets.Interfaces
{
    interface IMessageEvent
    {
        /// <summary>
        /// Handles the incoming packet.
        /// </summary>
        /// <param name="Session">Session to use</param>
        /// <param name="Packet">Message builded in packet.</param>
        void Invoke(Session Session, ClientPacket Packet);
    }
}
