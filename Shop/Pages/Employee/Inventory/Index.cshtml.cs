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

namespace Shop.Pages.Employee.Inventory
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
        public string? SearchString { get; set; }

        public SelectList? Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchCategory { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<string> categoryQuery = from item in _context.Inventory 
                                               orderby item.Category 
                                               select item.Category;

            var selectedItems = from item in _context.Inventory
                                orderby item.Category 
                                select item;

            if (!string.IsNullOrEmpty(SearchString))
            {
                selectedItems = (IOrderedQueryable<Item>) selectedItems.Where(s => s.Name.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(SearchCategory))
            {
                selectedItems = (IOrderedQueryable<Item>) selectedItems.Where(s => s.Category == SearchCategory);
            }

            Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());

            Item =  await selectedItems.ToListAsync();
        }
    }
}
