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
            public string? Description { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
            public int Amount { get; set; }
        }

        [BindProperty]
        public CartItemInfo Item { get; set; } = default!;

        public string ImageSource { get; set; }

        // Customer ID
        [BindProperty(SupportsGet = true)]
        public int CustId { get; set; }

        // Item ID
        [BindProperty(SupportsGet = true)]
        public int ItemId { get; set; }


        // Gets cart item info to display
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
                           Description = i.Description,
                           Category = i.Category,
                           Price = i.Price,
                           Amount = c.Amount
                       };

            ImageSource = _context.Inventory.Where(item => item.Id == ItemId).FirstOrDefault().ImageSource;

            Item =  await item.FirstOrDefaultAsync();
            
            if (Item == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        // Gets the new amount and sets the cart item's amount as that
        public async Task<IActionResult> OnPostAsync()
        {

            // If inputted data isn't good, restart
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
                               Description = i.Description,
                               Category = i.Category,
                               Price = i.Price,
                               Amount = c.Amount
                           };

                ImageSource = _context.Inventory.Where(item => item.Id == ItemId).FirstOrDefault().ImageSource;

                Item = await item1.FirstOrDefaultAsync();

                return Page();
            }

            var cartItem = from c in _context.Cart
                           where c.CustomerId == CustId && c.ItemId == ItemId
                           select c;

            var item = cartItem.FirstOrDefault();

            if (Item.Amount == 0)
            {
                var removedItem = _context.Cart.Where(item => item.CustomerId == CustId && item.ItemId == ItemId).FirstOrDefault();

                _context.Cart.Remove(removedItem);

                _context.SaveChanges();

                return RedirectToPage("./Index", new { CustId = CustId });
            }
            else if (Item.Amount < 0) { 
                return RedirectToPage("./Index", new { CustId = CustId });
            }

            item.Amount = Item.Amount;

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
