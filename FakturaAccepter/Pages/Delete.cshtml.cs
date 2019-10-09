using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FakturaAccepter.Data;
using FakturaAccepter.Models;

namespace FakturaAccepter.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly FakturaAccepter.Models.FakturaContext _context;

        public DeleteModel(FakturaAccepter.Models.FakturaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Faktura Faktura { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Faktura = await _context.Faktura.FirstOrDefaultAsync(m => m.Id == id);

            if (Faktura == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Faktura = await _context.Faktura.FindAsync(id);

            if (Faktura != null)
            {
                _context.Faktura.Remove(Faktura);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
