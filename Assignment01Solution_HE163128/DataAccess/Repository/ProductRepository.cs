using BusinessObjects;
using BusinessObjects.Models;
using DataAccess;
using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
	public class ProductRepository : IProductRepository
	{
		public void DeleteProduct(Product p) => ProductDAO.DeleteProduct(p);
        public void SaveProduct(ProductRequestDto p) => ProductDAO.SaveProduct(p);
        public void UpdateProduct(ProductUpdateRequestDto p) => ProductDAO.UpdateProduct(p);
        public List<Category> GetCategories() => CategoryDAO.GetCategories();
        public List<Product>? GetProducts(string? keyword, int? unitP) => ProductDAO.GetProducts(keyword, unitP);
        public Product GetProductById(int id) => ProductDAO.FindProductById(id);
	}
}
