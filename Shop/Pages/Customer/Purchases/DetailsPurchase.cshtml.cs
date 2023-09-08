using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Pages.Customer.Purchases
{
    public class DetailsPurchaseModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public DetailsPurchaseModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        public class CompletePurchaseInfo
        {
            public Purchase Purchase { get; set; }
            public Item Item { get; set; }
        }

        public CompletePurchaseInfo Purchase { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int CustId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int ItemId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.PurchaseHistory == null || _context.Inventory == null)
            {
                return NotFound();
            }

            var purchase = from p in _context.PurchaseHistory
                           join i in _context.Inventory on p.ItemId equals i.Id
                           where p.CustomerId == CustId && i.Id == ItemId
                           select new
                           CompletePurchaseInfo()
                           {
                               Purchase = p,
                               Item = i
                           };

            if (purchase == null || purchase.Count() == 0)
            {
                return NotFound();
            }
            else 
            {
                Purchase = purchase.FirstOrDefault();
            }

            return Page();
        }
    }
}
