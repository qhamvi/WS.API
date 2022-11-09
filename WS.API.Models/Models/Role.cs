using System;

namespace WS.API.Models
{
        public record Role
        {
            public Guid Id { get; set; }

            public string NameRole { get; set; }
        }
 
}
