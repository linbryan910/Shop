using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Pages.Employee.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public DetailsModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

      public CustomerAccount CustomerAccount { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CustAccounts == null)
            {
                return NotFound();
            }

            var customeraccount = await _context.CustAccounts.FirstOrDefaultAsync(m => m.Id == id);
            if (customeraccount == null)
            {
                return NotFound();
            }
            else 
            {
                CustomerAccount = customeraccount;
            }
            return Page();
        }
    }
}
