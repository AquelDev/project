using HabboEnvironment_R3.Kernel.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Kernel.Storage.Querys
{
    class CastCharacterQuery : Query
    {
        public CastCharacterQuery(int CharacterId)
        {
            base.Listen("SELECT * FROM server_users WHERE id = @id LIMIT 1", QueryType.DataRow);
            base.Push("id", CharacterId);
        }

        public CastCharacterQuery(string Username)
        {
            base.Listen("SELECT * FROM server_users WHERE username = @username LIMIT 1", QueryType.DataRow);
            base.Push("username", Username);
        }
    }
}
