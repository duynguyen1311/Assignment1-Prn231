using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eStoreClient.Utility;
using BusinessObjects.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStoreClient.Pages.ProductPage
{
    public class ProductModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string ProductApiUrl = "";
        public List<Product> ListProduct { get; set; }

        [BindProperty]
        public string? Keyword { get; set; }
        [BindProperty]
        public string? Price { get; set; }

        public ProductModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = _configuration.GetValue<string>("DomainURL") + "Product/GetAllProduct";
        }
        public async Task<IActionResult> OnGetAsync(string? keyword, string? price)
        {
            Keyword = keyword;
            Price = price;

            string url = ProductApiUrl+"?keyword="+keyword+"&unitP="+price;
            if(keyword == null && price == null)
            {
                url = ProductApiUrl;
            }
            HttpResponseMessage resp = await client.GetAsync(url);

            var strData = await resp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options);
            ListProduct = listProducts;
            return Page();
        }
    }
}
