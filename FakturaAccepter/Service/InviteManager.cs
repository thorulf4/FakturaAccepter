using FakturaAccepter.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakturaAccepter.Service
{
    public class InviteManager
    {
        private ApplicationDbContext context;

        public InviteManager(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<string> CreateNewInviteLink(string role)
        {
            string key = GenerateKey() + GenerateKey();
            InviteLink link = new InviteLink(key, role);

            DbSet<InviteLink> inviteLinks = context.InviteLinks;
            inviteLinks.Add(link);

            await context.SaveChangesAsync();

            return key;
        }

        public bool IsKeyValid(string key)
        {
            foreach(InviteLink link in context.InviteLinks)
            {
                if (link.Key == key && !IsExpired(link))
                    return true;
            }
            return false;
        }

        public string UseKey(string key)
        {
            InviteLink link = context.InviteLinks.First(l => l.Key == key);
            if (link != null && !IsExpired(link))
            {
                context.InviteLinks.Remove(link);
                context.SaveChanges();
                return link.Role;
            }

            throw new Exception("Invalid key used");
        }

        public bool IsExpired(InviteLink link)
        {
            return link.ExpiryTime.Subtract(DateTime.UtcNow).TotalSeconds <= 0;
        }

        private string GenerateKey()
        {
            Guid g = Guid.NewGuid();
            string guidString = Convert.ToBase64String(g.ToByteArray());
            guidString = guidString.Replace("=", "");
            guidString = guidString.Replace("+", "");
            guidString = guidString.Replace("/", "");
            guidString = guidString.Replace("\\", "");

            return guidString.ToLower();
        }
    }
}
