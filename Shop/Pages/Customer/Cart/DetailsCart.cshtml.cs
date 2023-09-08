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
    public class DetailsCartModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public DetailsCartModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        public class CartItemInfo 
        { 
            public int Id { get; set; }
            public int ItemId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
            public int Amount { get; set; }
        }

        public CartItemInfo CartItem { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int CustId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ItemId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Inventory == null)
            {
                return NotFound();
            }

            var cartItem = from i in _context.Inventory
                       join c in _context.Cart on i.Id equals c.ItemId
                       where c.CustomerId == CustId && c.ItemId == ItemId
                       select new
                       CartItemInfo()
                       {
                           Id = c.Id,
                           ItemId = i.Id, 
                           Name = i.Name,
                           Description = i.Description,
                           Category = i.Category,
                           Price = i.Price,
                           Amount = c.Amount
                       };

            if (cartItem == null)
            {
                return NotFound();
            }
            else
            {
                CartItem = await cartItem.FirstOrDefaultAsync();
            }

            return Page();
        }
    }
}
