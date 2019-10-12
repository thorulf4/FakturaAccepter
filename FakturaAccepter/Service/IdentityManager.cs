using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakturaAccepter.Service
{
    public class IdentityManager
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;

        public IdentityManager(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public void CheckForInitialUser()
        {
            if (userManager.FindByNameAsync("Admin").GetAwaiter().GetResult() == null)
            {
                var user = new IdentityUser() { UserName = "Admin", Email = "thorulf@live.dk" };
                user.EmailConfirmed = true;
                userManager.CreateAsync(user, "Admin123!").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
            }
        }

        public void AddRole(string role)
        {
            bool roleExists = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();

            if (!roleExists)
            {
                roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
            }
        }
    }
}
