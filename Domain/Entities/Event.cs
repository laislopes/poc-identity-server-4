using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Profile_Event> Profiles { get; set; }
        public Event()
        {

        }

        public Event(string nome)
        {
            Nome = nome;
        }
    }
}
