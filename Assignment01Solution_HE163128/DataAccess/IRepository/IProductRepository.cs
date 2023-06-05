using BusinessObjects;
using BusinessObjects.Models;
using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
	public interface IProductRepository
	{
		void SaveProduct(ProductRequestDto p);
		Product GetProductById(int id);
		void DeleteProduct(Product p);
		void UpdateProduct(ProductUpdateRequestDto p);
		List<Category> GetCategories();
		List<Product>? GetProducts(string? keyword, int? unitP);
	}
}
