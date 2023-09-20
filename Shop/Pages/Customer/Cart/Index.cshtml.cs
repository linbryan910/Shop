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
    public class IndexModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public IndexModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        public class CartItemInfo 
        { 
            public int Id { get; set; }
            public int ItemId { get; set; }
            public string Name { get; set; }
            public string ImageSource { get; set; }
            public string? Description { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
            public int Amount { get; set; }
        }

        public IList<CartItemInfo> CartInfo { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int CustId { get; set; }

        public decimal TotalPrice { get; set; } = 0;

        public async Task OnGetAsync()
        {

            if (_context.Cart != null)
            {
                var cartInfo = from i in _context.Inventory
                           join c in _context.Cart on i.Id equals c.ItemId
                           where c.CustomerId == this.CustId
                           select new
                           CartItemInfo(){
                               Id = c.Id, 
                               ItemId = i.Id,
                               Name = i.Name,
                               ImageSource = i.ImageSource,
                               Description = i.Description,
                               Category = i.Category,
                               Price = i.Price,
                               Amount = c.Amount
                           };

                CartInfo = await cartInfo.ToListAsync();

                TotalPrice = 0;
                foreach (var item in CartInfo) {
                    TotalPrice += item.Price * item.Amount;
                }
            }
        }

        public async Task<IActionResult> OnPostRemove(int RemovedCartItemId) 
        {
            var removedCartItem = _context.Cart.Find(RemovedCartItemId);

            if (removedCartItem is not null)
            {
                _context.Cart.Remove(removedCartItem);

                System.Diagnostics.Debug.WriteLine(RemovedCartItemId);

                _context.SaveChanges();

                return RedirectToPage("./Index", new { CustId = CustId });
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostPurchase(int CustId) 
        {
            var cart = _context.Cart.Where(item => item.CustomerId == CustId);

            IList<CartItem> cartAsList = cart.ToList();

            foreach (var item in cartAsList) 
            {
                var i = _context.Inventory.Find(item.ItemId);

                if (i.Amount < item.Amount) 
                {
                    _context.ChangeTracker.Clear();

                    return RedirectToPage("./ErrorCart", new { CustId = CustId, ItemId = item.ItemId});
                }

                i.Amount -= item.Amount;
                _context.Inventory.Attach(i);
            }

            foreach (var item in cartAsList)
            {
                var purchase = new Shop.Models.Purchase() { CustomerId = item.CustomerId, ItemId = item.ItemId, Amount = item.Amount, Date = DateTime.Now };

                _context.PurchaseHistory.Add(purchase);
            }

            foreach (var item in cartAsList) 
            {
                _context.Cart.Remove(item);
            }

            _context.SaveChanges();

            return RedirectToPage("../Browse/Index", new { CustId = CustId });
        }
    }
}
