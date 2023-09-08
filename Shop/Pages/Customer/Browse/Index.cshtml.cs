using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Pages.Customer.Browse
{
    public class IndexModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public IndexModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int CustId { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Inventory != null)
            {
                Item = await _context.Inventory.ToListAsync();
            }
        }
    }
}
