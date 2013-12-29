using habbo.Habbo.Characters;
using habbo.Kernel.IO;
using habbo.Kernel.Network;
using habbo.Kernel.Packets;
using HabboEnvironment_R3.Kernel.IO;
using HabboEnvironment_R3.Kernel.Network;
using HabboEnvironment_R3.Kernel.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace habbo
{
    //444bdc7
    static class SystemApp
    {
        public static ConsoleSystem ConsoleSystem;
        public static SocketSystem SocketSystem;
        public static PacketManager PacketManager;
        public static MySQLManager MySQLManager;
        public static CharacterManager CharacterManager;
        public static MusListener MusListener
        {
            get { return MusListener.GetMusListener(30001); }
        }

        private static readonly Dictionary<string, List<string>> BannerTokensHolder = new Dictionary<string, List<string>>();

        public static Dictionary<string, List<string>> BannerTokens
        {
            get { return BannerTokensHolder; }
        }

        static void Main(string[] args)
        {
            ConsoleSystem = new ConsoleSystem();
            SocketSystem = new SocketSystem();
            CharacterManager = new CharacterManager();
            MySQLManager = new MySQLManager("127.0.0.1", 3306, "root", "lol123", "bear", true, 5, 30);
            ConsoleSystem.Serialize("habbo - Sulake better get it coming!");
            ConsoleSystem.PrintLine("BOOT", @"   _           _     _           ");
            ConsoleSystem.PrintLine("BOOT", @"  | |         | |   | |          ");
            ConsoleSystem.PrintLine("BOOT", @"  | |__  _____| |__ | |__   ___  ");
            ConsoleSystem.PrintLine("BOOT", @"  |  _ \(____ |  _ \|  _ \ / _ \  - sulake better get it coming");
            ConsoleSystem.PrintLine("BOOT", @"  | | | / ___ | |_) ) |_) ) |_| |  ["+VersionInfo.Version+"]");
            ConsoleSystem.PrintLine("BOOT", @"  |_| |_\_____|____/|____/ \___/");
            ConsoleSystem.PrintLine("BOOT", @"-----------------------------------------------------------------");

            SocketSystem.ConstructSocket(IPAddress.Parse("127.0.0.1"), 30000, 10);
            SocketSystem.ConstructPooling(10000);

            MusListener.Start();

            PacketManager = new PacketManager();

            while (true) Console.Read();
        }
    }
}