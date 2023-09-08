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
    public class IndexModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public IndexModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<EmployeeAccount> EmployeeAccount { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.EmplAccounts != null)
            {
                EmployeeAccount = await _context.EmplAccounts.ToListAsync();
            }
        }
    }
}
