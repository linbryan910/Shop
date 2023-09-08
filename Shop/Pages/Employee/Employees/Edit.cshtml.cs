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

namespace Shop.Pages.Employee.Employees
{
    public class EditModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public EditModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EmployeeAccount EmployeeAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.EmplAccounts == null)
            {
                return NotFound();
            }

            var employeeaccount =  await _context.EmplAccounts.FirstOrDefaultAsync(m => m.Id == id);
            if (employeeaccount == null)
            {
                return NotFound();
            }
            EmployeeAccount = employeeaccount;
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

            _context.Attach(EmployeeAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeAccountExists(EmployeeAccount.Id))
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

        private bool EmployeeAccountExists(int id)
        {
          return (_context.EmplAccounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
