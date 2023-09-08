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
    public class DetailsModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public DetailsModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public Item Item { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int CustId { get; set; }

        [BindProperty]
        public Shop.Models.CartItem CartItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Inventory == null)
            {
                return NotFound();
            }

            var item = await _context.Inventory.FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            else 
            {
                Item = item;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CartItem.CustomerId = CustId;
            CartItem.ItemId = Item.Id;

            var cartItem = from item in _context.Cart
                           where item.CustomerId == CartItem.CustomerId && item.ItemId == CartItem.ItemId 
                           select item;

            if (cartItem.Count() > 0)
            {
                cartItem.First().Amount += CartItem.Amount;
            }
            else
            {
                _context.Cart.Add(CartItem);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { CustId = CustId });
        }
    }
}
