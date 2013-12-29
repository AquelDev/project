using habbo.Cryptography;
using habbo.Kernel.Commons;
using habbo.Kernel.Network;
using habbo.Kernel.Packets.com.sulake.habbo.communication.messages.incoming.handshake;
using habbo.Kernel.Packets.Interfaces;
using habbo.Kernel.Packets.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Packets
{
    class PacketManager
    {
        /// <summary>
        /// Storage of names from Info Events
        /// </summary>
        public Dictionary<string, short> InfoEvents;

        /// <summary>
        /// Storage of Invokers from Message Events
        /// </summary>
        public Dictionary<short, IMessageEvent> MessageEvents;

        public PacketManager()
        {
            InfoEvents = new Dictionary<string, short>();

            foreach (var Packet in typeof(Headers.MessageEventIds).GetFields())
            {
                var PacketId = (short)Packet.GetValue(0);
                var PacketName = Packet.Name;

                if (!InfoEvents.ContainsValue(PacketId))
                {
                    InfoEvents.Add(PacketName, PacketId);
                }
            }

            MessageEvents = new Dictionary<short, IMessageEvent>();

            foreach (Type Type in Assembly.GetCallingAssembly().GetTypes())
            {
                if (Type == null)
                {
                    continue;
                }

                if (Type.GetInterfaces().Contains(typeof(IMessageEvent)))
                {
                    var ConstructorInfo = Type.GetConstructor(new Type[] { });

                    if (ConstructorInfo != null)
                    {
                        var Constructed = ConstructorInfo.Invoke(new object[] { }) as IMessageEvent;

                        short Header = GetHeader(Constructed);

                        if (!MessageEvents.ContainsKey(Header) && Header > 0)
                        {
                            MessageEvents.Add(Header, Constructed);
                        }
                    }
                }
            }

            int ComposerIds = typeof(Headers.MessageComposerIds).GetFields().Count();

            Console.WriteLine("Loaded {0}/{1} MessageEvent(s), and {2} MessageComposer(s).", MessageEvents.Count, InfoEvents.Count, ComposerIds);
        }

        /// <summary>
        /// Returns an header from an InfoEvent.
        /// </summary>
        /// <param name="Event"></param>
        /// <returns></returns>
        public short GetHeader(IMessageEvent Event)
        {
            using (DictionaryAdapter<string, short> DA = new DictionaryAdapter<string, short>(InfoEvents))
            {
                return DA.TryPopValue(Event.GetType().Name);
            }
        }

        /// <summary>
        /// Returns an name of an InfoEvent.
        /// </summary>
        /// <param name="Header"></param>
        /// <returns></returns>
        public string GetName(short Header)
        {
            using (DictionaryAdapter<string, short> DA = new DictionaryAdapter<string, short>(InfoEvents))
            {
                return DA.TryPopKey(Header);
            }
        }

        /// <summary>
        /// Handles the bytes from an Session.
        /// </summary>
        /// <param name="Session"></param>
        /// <param name="Bytes"></param>
        public void ProcessBytes(Session Session, ref byte[] Bytes)
        {
            try
            {
                bool isPolicy = Bytes[0] == 60;

                if (isPolicy == false)
                {
                    int pos = 0;

                    if (Session.HabboEncryption != null && Session.HabboEncryption.Initialized)
                    {
                        Bytes = Session.HabboEncryption.Rc4.Decipher(Bytes);
                    }

                    while (pos < Bytes.Length)
                    {

                        int MessageLength = HabboEncoding.DecodeInt32(new byte[] { Bytes[pos++], Bytes[pos++], Bytes[pos++], Bytes[pos++] });
                        int MessageId = HabboEncoding.DecodeInt16(new byte[] { Bytes[pos++], Bytes[pos++] });
                        try
                        {
                            byte[] Content = new byte[MessageLength - 2];

                            for (int i = 0; i < Content.Length; i++)
                            {
                                Content[i] = Bytes[pos++];
                            }

                            ClientPacket pak = new ClientPacket(MessageId, Content);
                            IMessageEvent Event = MessageEvents[pak.Header()];
                            if (MessageEvents.ContainsKey(pak.Header()))
                            {
                                SystemApp.ConsoleSystem.PrintLine("[RCV][#{0}][" + GetName(pak.Header()) + "]", pak.Header());
                                Event.Invoke(Session, pak);
                            }
                        }
                        catch (EntryPointNotFoundException e)
                        {
                            SystemApp.ConsoleSystem.PrintLine(e.ToString());
                        }
                        catch (KeyNotFoundException ke)
                        {
                            SystemApp.ConsoleSystem.PrintLine("[RCV][#" + MessageId + "] >> Not found");
                        }
                    }
                }
                else if (isPolicy == true)
                {
                    SystemApp.ConsoleSystem.PrintLine("Policy request received.");
                    byte[] policy = Encoding.Default.GetBytes("<?xml version=\"1.0\"?>\r\n" +
                       "<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n" +
                       "<cross-domain-policy>\r\n" +
                       "<allow-access-from domain=\"*\" to-ports=\"1-31111\" />\r\n" +
                       "</cross-domain-policy>\x0");
                    Session.Socket.Send(policy);
                }
            }
            catch (IndexOutOfRangeException ie)
            {
                SystemApp.ConsoleSystem.PrintLine("[ProcessBytes] {0}", ie.ToString());
            }
        }
    }
}
