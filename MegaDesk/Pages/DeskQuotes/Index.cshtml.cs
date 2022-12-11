using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDesk.Data;
using MegaDesk.Models;

namespace MegaDesk.Pages.DeskQuotes
{
    public class IndexModel : PageModel
    {
        private readonly MegaDesk.Data.MegaDeskContext _context;

        public IndexModel(MegaDesk.Data.MegaDeskContext context)
        {
            _context = context;
        }

        public IList<DeskQuote> DeskQuote { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchString { get;set; }


        public async Task OnGetAsync()
        {
            if (_context.DeskQuote != null)
            {
                DeskQuote = await _context.DeskQuote
                .Include(d => d.DeliveryType)
                .Include(d => d.Desk)
                .Include(d => d.Desk.DesktopMaterial)
                .ToListAsync();
            }
            
            var deskQuote = from d in _context.DeskQuote
                            select d;

            if (!string.IsNullOrEmpty(SearchString))
            {
                deskQuote = deskQuote.Where(d => d.CustomerName.Contains(SearchString));
                DeskQuote = await deskQuote.ToListAsync();
            }
            
        }
    }
}
