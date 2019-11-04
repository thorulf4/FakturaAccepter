using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakturaAccepter.Data
{
    public class InviteLink
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Role { get; set; }
        public DateTime ExpiryTime { get; set; }

        public InviteLink(string key, string role)
        {
            Key = key;
            Role = role;
            ExpiryTime = DateTime.UtcNow.AddDays(7);
        }
    }
}
