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
    public class IndexModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public IndexModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<CustomerAccount> CustomerAccount { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CustAccounts != null)
            {
                CustomerAccount = await _context.CustAccounts.ToListAsync();
            }
        }
    }
}
