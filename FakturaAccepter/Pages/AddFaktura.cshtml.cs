using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FakturaAccepter.Data;
using FakturaAccepter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FakturaAccepter.Pages
{
    public class AddFakturaModel : PageModel
    {
        private readonly FakturaContext context;

        public AddFakturaModel(FakturaContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    await SaveFile(formFile);
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return new OkResult();
        }

        private async Task SaveFile(IFormFile formFile)
        {
            Faktura faktura = new Faktura();
            faktura.Name = formFile.FileName;
            faktura.State = FakturaState.Pending;
            faktura.TimeAdded = DateTime.Now;

            await context.AddAsync(faktura);
            await context.SaveChangesAsync();

            var filePath = $"FakturaFiles/{faktura.Id}.pdf";

            using (var stream = System.IO.File.Create(filePath))
            {
                await formFile.CopyToAsync(stream);
            }
        }
    }
}