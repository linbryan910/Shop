using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Pages.Customer.Cart
{
    public class EditCartModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public EditCartModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        public class CartItemInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ImageSource { get; set; }
            public string? Description { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
            public int Amount { get; set; }
        }

        public CartItemInfo Item { get; set; } = default!;

        public int NewAmount { get; set; }

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

            var item = from i in _context.Inventory
                       join c in _context.Cart on i.Id equals c.ItemId
                       where c.CustomerId == CustId && c.ItemId == ItemId
                       select new
                       CartItemInfo()
                       {
                           Id = i.Id,
                           Name = i.Name,
                           ImageSource = i.ImageSource,
                           Description = i.Description,
                           Category = i.Category,
                           Price = i.Price,
                           Amount = c.Amount
                       };

            Item =  await item.FirstOrDefaultAsync();
            
            if (Item == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                var item1 = from i in _context.Inventory
                           join c in _context.Cart on i.Id equals c.ItemId
                           where c.CustomerId == CustId && c.ItemId == ItemId
                           select new
                           CartItemInfo()
                           {
                               Id = i.Id,
                               Name = i.Name,
                               ImageSource = i.ImageSource,
                               Description = i.Description,
                               Category = i.Category,
                               Price = i.Price,
                               Amount = c.Amount
                           };

                Item = await item1.FirstOrDefaultAsync();

                return Page();
            }

            var cartItem = from c in _context.Cart
                           where c.CustomerId == CustId && c.ItemId == ItemId
                           select c;

            var item = cartItem.FirstOrDefault();

            item.Amount = NewAmount;

            _context.Attach(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(Item.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { CustId = CustId });
        }

        private bool ItemExists(int id)
        {
          return (_context.Inventory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
