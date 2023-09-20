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
    public class IndexModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public IndexModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        public class BasicPurchaseInfo 
        { 
            public Purchase Purchase { get; set; }
            public string ItemName { get; set; }
            public string ImageSource { get; set; }
        }

        public IList<BasicPurchaseInfo> Purchases { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int CustId { get; set; }

        public async Task OnGetAsync()
        {

            if (_context.PurchaseHistory != null)
            {
                var purchases = from purchase in _context.PurchaseHistory
                                join item in _context.Inventory on purchase.ItemId equals item.Id
                                where purchase.CustomerId == CustId 
                                select new
                                BasicPurchaseInfo ()
                                {
                                    Purchase = purchase,
                                    ItemName = item.Name,
                                    ImageSource = item.ImageSource
                                };

                Purchases = await purchases.ToListAsync();
            }
        }
    }
}
