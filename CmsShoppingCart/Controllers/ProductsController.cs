﻿using CmsShoppingCart.DAL.Infrastructure;
using CmsShoppingCart.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly CmsShoppingCartContext context;

        public ProductsController(CmsShoppingCartContext context)
        {
            this.context = context;
        }

        // GET /products
        [Route("/products")]
        public async Task<IActionResult> Index(int p = 1)
        {

            int pageSize = 6;
            var products = await context.Products.OrderByDescending(x => x.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Products.Count() / pageSize);

            return View(products);
        }

        // GET /products/category
        [Route("/products/category")]
        public async Task<IActionResult> ProductsByCategory(string categorySlug, int p = 1)
        {

            Category category = await context.Categories.Where(x => x.Slug == categorySlug).FirstOrDefaultAsync();

            if (category == null) return RedirectToAction("Index");
            
            int pageSize = 6;
            var products = context.Products.OrderByDescending(x => x.Id)
                                        .Where(x => x.CategoryId == category.Id)
                                        .Skip((p - 1) * pageSize)
                                        .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Products.Where(x => x.CategoryId == category.Id).Count() / pageSize);
            ViewBag.CategoryName = category.Name;
            ViewBag.CategorySlug = category.Slug;

            return View(await products.ToListAsync());
        }
    }
}
