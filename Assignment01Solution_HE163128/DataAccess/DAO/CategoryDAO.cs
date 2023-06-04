using BusinessObjects;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class CategoryDAO
	{
		public static List<Category> GetCategories()
		{
			var ListCategories = new List<Category>();
			try
			{
				using (var context = new PRN231_AS1Context())
				{
					ListCategories = context.Categories.ToList();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			return ListCategories;
		}
	}
}
