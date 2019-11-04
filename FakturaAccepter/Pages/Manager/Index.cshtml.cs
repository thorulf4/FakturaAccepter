using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakturaAccepter.Data;
using FakturaAccepter.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FakturaAccepter.Pages.Manager
{
    [Authorize(Roles = "Manager,Admin")]
    public class IndexModel : PageModel
    {
        private ApplicationDbContext applicationContext;
        private InviteManager inviteManager;

        public IndexModel(ApplicationDbContext applicationContext, InviteManager inviteManager)
        {
            this.applicationContext = applicationContext;
            this.inviteManager = inviteManager;
        }

        public IList<InviteLink> InviteLinks { get; set; }

        public void OnGet()
        {
            GetInviteLinks();
        }

        private void GetInviteLinks()
        {
            InviteLinks = applicationContext.InviteLinks.ToList();
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            InviteLink link = (InviteLink) await applicationContext.FindAsync(typeof(InviteLink) ,new object[] { id });
            applicationContext.InviteLinks.Remove(link);
            await applicationContext.SaveChangesAsync();
            return RedirectToAction("");
        }

        public async Task<IActionResult> OnGetCreateMemberLink()
        {
            await inviteManager.CreateNewInviteLink("Member");
            return RedirectToAction("");
        }
    }
}