using habbo.Kernel.Commons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habbo.Habbo.Characters
{
    public class Character
    {
        /// <summary>
        /// Id of the character.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Username of the character.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Motto of the character.
        /// </summary>
        public string Motto { get; private set; }

        /// <summary>
        /// Figure of the character.
        /// </summary>
        public string Figure { get; private set; }


        public Character(DataRow Row)
        {
            using (RowAdapter Adapter = new RowAdapter(Row))
            {
                this.Id = Adapter.PopInt32("id");
                this.Username = Adapter.PopString("username");
                this.Motto = Adapter.PopString("motto");
                this.Figure = Adapter.PopString("look");
            }
        }
    }
}
