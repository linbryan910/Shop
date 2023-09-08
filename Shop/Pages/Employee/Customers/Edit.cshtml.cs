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

namespace Shop.Pages.Employee.Customers
{
    public class EditModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public EditModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CustomerAccount CustomerAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CustAccounts == null)
            {
                return NotFound();
            }

            var customeraccount =  await _context.CustAccounts.FirstOrDefaultAsync(m => m.Id == id);
            if (customeraccount == null)
            {
                return NotFound();
            }
            CustomerAccount = customeraccount;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CustomerAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAccountExists(CustomerAccount.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CustomerAccountExists(int id)
        {
          return (_context.CustAccounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
