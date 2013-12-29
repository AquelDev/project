using HabboEnvironment_R3.Kernel.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Storage.Querys
{
    class OnlineCountUpdate : Query
    {
        public OnlineCountUpdate(bool PlusOrMinus)
        {
            if (PlusOrMinus)
            {
                base.Listen("UPDATE `server_stats` SET `value` = `value`+1 WHERE `key` = 'online_count'", QueryType.Action);
            }
            else
            {
                base.Listen("UPDATE `server_stats` SET `value` = `value`-1 WHERE `key` = 'online_count'", QueryType.Action);
            }
        }
    }
}
