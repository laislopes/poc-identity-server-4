using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Profile_Event> Events { get; set; }

        public Profile()
        {

        }

        public Profile(string nome)
        {
            Nome = nome;
        }
    }
}
