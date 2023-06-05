using BusinessObjects;
using BusinessObjects.Models;
using DataAccess.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        public static List<Product>? GetProducts(string? keyword, int? unitP)
        {
            List<Product>? listProducts = null;
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var query = context.Products.AsQueryable();
                    if (keyword != null)
                    {
                        query = query.Where(i => !string.IsNullOrEmpty(i.ProductName) && i.ProductName.ToLower().Contains(keyword.ToLower()));
                    }
                    if(unitP > 0)
                    {
                        query = query.Where(i => i.UnitPrice == unitP);
                    }

                    listProducts = query.Include(i => i.Category).AsNoTracking().ToList();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listProducts;
        }

        public static Product FindProductById(int prodId)
        {
            Product p = new Product();
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    p = context.Products.Include(i => i.Category).AsNoTracking().SingleOrDefault(x => x.ProductId == prodId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }

        public static void SaveProduct(ProductRequestDto p)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var prod = new Product()
                    {
                        CategoryId = p.CategoryId,
                        ProductName = p.ProductName,
                        Weight = p.Weight,
                        UnitPrice = p.UnitPrice,
                        UnitsInStock = p.UnitsInStock
                    };
                    context.Products.Add(prod);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateProduct(ProductUpdateRequestDto p)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var prod = new Product()
                    {
                        ProductId = p.ProductId,
                        CategoryId = p.CategoryId,
                        ProductName = p.ProductName,
                        Weight = p.Weight,
                        UnitPrice = p.UnitPrice,
                        UnitsInStock = p.UnitsInStock
                    };
                    context.Entry<Product>(prod).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteProduct(Product p)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var p1 = context.Products.SingleOrDefault(
                        c => c.ProductId == p.ProductId);
                    context.Products.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
