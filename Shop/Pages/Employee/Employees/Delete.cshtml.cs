using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Pages.Employee.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public DeleteModel(Shop.Data.ShopContext context)
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

            var employeeaccount = await _context.EmplAccounts.FirstOrDefaultAsync(m => m.Id == id);

            if (employeeaccount == null)
            {
                return NotFound();
            }
            else 
            {
                EmployeeAccount = employeeaccount;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.EmplAccounts == null)
            {
                return NotFound();
            }
            var employeeaccount = await _context.EmplAccounts.FindAsync(id);

            if (employeeaccount != null)
            {
                EmployeeAccount = employeeaccount;
                _context.EmplAccounts.Remove(EmployeeAccount);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
