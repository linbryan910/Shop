using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Data;
using Shop.Models;

namespace Shop.Pages.Employee.Inventory
{
    public class CreateModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public CreateModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; } = default!;

        public Boolean IfItemAlreadyExists = false;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Inventory == null || Item == null)
            {
                return Page();
            }

            var Items = from item in _context.Inventory
                        where item.Name == Item.Name
                        select item;

            if (Items.Count() > 0) {
                IfItemAlreadyExists = true;
                return Page();
            }

            _context.Inventory.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
