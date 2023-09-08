using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Shop.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public IndexModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public string AccountType { get; set; }
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnPost()
        {
            if (AccountType.Equals("Customer")) {
                var account = from acc in _context.CustAccounts
                          where acc.UserName == UserName && acc.Password == Password
                          select acc;

                if (account.Count() == 0) {
                    return Page();
                }

                return new RedirectToPageResult("../Customer/Browse/Index", new { CustId = account.First().Id});
            }
            else if (AccountType.Equals("Employee")) {
                var account = from acc in _context.EmplAccounts
                          where acc.UserName == UserName && acc.Password == Password
                          select acc;

                if (account.Count() == 0) { 
                    return Page(); 
                }

                return new RedirectToPageResult("../Employee/Inventory/Index", new { CustId = account.First().Id });
            }

            return NotFound();
        }
    }
}
