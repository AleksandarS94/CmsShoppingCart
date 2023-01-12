﻿
using CmsShoppingCart.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CmsShoppingCart.DAL.Infrastructure
{
    public class CmsShoppingCartContext : IdentityDbContext<AppUser>
    {
        public CmsShoppingCartContext(DbContextOptions<CmsShoppingCartContext> options )
            :base(options)
        { 
        }
        public DbSet<Page> Pages{ get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Product> Products{ get; set; }
    }
}
