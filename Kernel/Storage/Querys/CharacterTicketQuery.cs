using HabboEnvironment_R3.Kernel.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Storage.Querys
{
    class CharacterTicketQuery : Query
    {
        public CharacterTicketQuery(string Ticket)
        {
            base.Listen("SELECT * FROM `server_users` WHERE `client_key` = @sso", QueryType.DataRow);
            base.Push("sso", Ticket);
        }
    }
}
