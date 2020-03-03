using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Profile_Event
    {
        public int ProfileId { get; set; }
        public int EventId { get; set; }
        public Profile Profile { get; set; }
        public Event Event { get; set; }

        public Profile_Event()
        {

        }

        public Profile_Event(int profileId, int eventId)
        {
            ProfileId = profileId;
            EventId = eventId;
        }
    }
}
