using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FakturaAccepter.Data;
using FakturaAccepter.Models;
using System.IO;

namespace FakturaAccepter.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FakturaAccepter.Models.FakturaContext context;

        public IndexModel(FakturaAccepter.Models.FakturaContext context)
        {
            this.context = context;
        }

        public IList<Faktura> Faktura { get;set; }

        public async Task OnGetAsync()
        {
            Faktura = await context.Faktura.ToListAsync();
        }

        public IActionResult OnGetFaktura(int id)
        {
            FileStream file = new FileStream($"FakturaFiles/{id}.pdf", FileMode.Open);

            return File(file, "application/pdf");
        }

        public IActionResult OnGetAccept(int id)
        {
            Faktura = context.Faktura.ToList();
            ChangeFakturaState(id, FakturaState.Accepted);

            return Page();
        }

        public IActionResult OnGetDeny(int id)
        {
            Faktura = context.Faktura.ToList();
            ChangeFakturaState(id, FakturaState.Denied);

            return Page();
        }

        private void ChangeFakturaState(int id, FakturaState newState)
        {
            Faktura faktura = Faktura.First(f => f.Id == id);
            faktura.State = newState;
            context.SaveChanges();
        }
    }
}
