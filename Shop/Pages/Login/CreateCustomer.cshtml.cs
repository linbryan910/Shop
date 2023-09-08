﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Data;
using Shop.Models;

namespace Shop.Pages.Login
{
    public class CreateCustomerModel : PageModel
    {
        private readonly Shop.Data.ShopContext _context;

        public CreateCustomerModel(Shop.Data.ShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CustomerAccount CustomerAccount { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.CustAccounts == null || CustomerAccount == null)
            {
                return Page();
            }

            _context.CustAccounts.Add(CustomerAccount);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
