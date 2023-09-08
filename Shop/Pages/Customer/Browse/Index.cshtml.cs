using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public SelectList Categories { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchCategory { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Gets all categories for items
            var categories = from i in _context.Inventory
                             orderby i.Category
                             select i.Category;

            Categories = new SelectList(await categories.Distinct().ToListAsync());

            // Gets items (filters by category/name if inputted)
            var item = from i in _context.Inventory
                       select i;

            if (!SearchCategory.IsNullOrEmpty()) {
                item = item.Where(i => i.Category == SearchCategory);
            }

            if (!SearchString.IsNullOrEmpty()) {
                item = item.Where(i => i.Name.Contains(SearchString));
            }

            Item = await item.ToListAsync();
        }
    }
}
