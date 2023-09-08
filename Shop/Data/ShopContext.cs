using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext (DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public DbSet<Shop.Models.Item> Inventory { get; set; } = default!;
        public DbSet<Shop.Models.EmployeeAccount> EmplAccounts { get; set; } = default!;
        public DbSet<Shop.Models.CustomerAccount> CustAccounts { get; set; } = default!;
        public DbSet<Shop.Models.CartItem> Cart { get; set; } = default!;
        public DbSet<Shop.Models.Purchase> PurchaseHistory { get; set; } = default!;
    }
}
