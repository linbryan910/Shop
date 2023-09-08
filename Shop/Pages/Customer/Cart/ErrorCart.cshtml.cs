using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Pages.Customer.Cart
{
    public class ErrorCartModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public ErrorCartModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int CustId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int ItemId { get; set; }

        public class CartErrorInfo
        {
            public Item Item { get; set; }
            public CartItem Cart { get; set; }
        }

        public CartErrorInfo Info { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.Inventory == null)
            {
                return NotFound();
            }

            var info = from i in _context.Inventory
                       join c in _context.Cart on i.Id equals c.ItemId
                       where i.Id == ItemId && c.CustomerId == CustId
                       select new
                       CartErrorInfo()
                       {
                           Item = i,
                           Cart = c
                       };

            if (info == null) {
                return NotFound();
            }

            Info = info.FirstOrDefault();

            return Page();
        }
    }
}
