using habbo.Kernel.Storage.Querys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Habbo.Characters
{
    class CharacterManager
    {
        /// <summary>
        /// Cache for offline users.
        /// </summary>
        public Dictionary<int, Character> WeakSQLCache { get; private set; }

        public CharacterManager()
        {
            WeakSQLCache = new Dictionary<int, Character>();
        }

        public bool GetCharacter(int CharacterId, out Character Character)
        {
            Character = null;

            foreach (Character xCharacter in WeakSQLCache.Values)
            {
                if (xCharacter.Id == CharacterId)
                {
                    Character = xCharacter;
                }
            }

            if (Character != null)
            {
                return true;
            }
            else
            {
                DataRow Obj = SystemApp.MySQLManager.GetObject(new CastCharacterQuery(CharacterId)).GetOutput<DataRow>();

                if (Obj != null)
                {
                    Character = new Character(Obj);
                    WeakSQLCache.Add(Character.Id, Character);
                }

                return Character != null;
            }
        }

        public bool GetCharacter(string Username, out Character Character)
        {
            Character = null;

            foreach (Character xCharacter in WeakSQLCache.Values)
            {
                if (xCharacter.Username.ToLower() == Username.ToLower())
                {
                    Character = xCharacter;
                }
            }

            if (Character != null)
            {
                return true;
            }
            else
            {
                DataRow Obj = SystemApp.MySQLManager.GetObject(new CastCharacterQuery(Username)).GetOutput<DataRow>();

                if (Obj != null)
                {
                    Character = new Character(Obj);
                    WeakSQLCache.Add(Character.Id, Character);
                }

                return Character != null;
            }
        }

        public Character Authenticate(string AuthTicket)
        {
            DataRow Obj = SystemApp.MySQLManager.GetObject(new CharacterTicketQuery(AuthTicket)).GetOutput<DataRow>();

            if (Obj != null)
            {
                return new Character(Obj);
            }

            return null;
        }
    }
}
