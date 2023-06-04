using BusinessObjects.Models;
using eStoreClient.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace eStoreClient.Pages.ProductPage
{
    public class UpdateProductModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string UpdateProductApiUrl = "";
        private string ProductCateApiUrl = "";
        private string ProductDetailApiUrl = "";

        [BindProperty]
        public Product Product { get; set; }
        /*[BindProperty]
        public ProductUpdateRequest ProductUpdateRequest { get; set; }*/
        public List<Category> listCategory { get; set; }


        public UpdateProductModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            ProductDetailApiUrl = _configuration.GetValue<string>("DomainURL") + "Product/Detail/{id}";
            UpdateProductApiUrl = _configuration.GetValue<string>("DomainURL") + "Product/Update";
            ProductCateApiUrl = _configuration.GetValue<string>("DomainURL") + "Product/GetAllCategory";
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {

            HttpResponseMessage resp = await client.GetAsync(ProductCateApiUrl);

            var strData = await resp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            listCategory = System.Text.Json.JsonSerializer.Deserialize<List<Category>>(strData, options);

            string requestUrl = ProductDetailApiUrl.Replace("{id}", id);
            HttpResponseMessage res = await client.GetAsync(requestUrl);
            var strData1 = await res.Content.ReadAsStringAsync();
            var options1 = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Product = System.Text.Json.JsonSerializer.Deserialize<Product>(strData1, options1);

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var request = new
            {
                ProductId = Product.ProductId,
                ProductName = Product.ProductName,
                CategoryId = Product.CategoryId,
                Weight = Product.Weight,
                UnitPrice = Product.UnitPrice,
                UnitsInStock = Product.UnitsInStock
            };

            string body = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            // Make the POST request using HttpClient with the request body
            HttpResponseMessage response = await client.PutAsync(UpdateProductApiUrl, httpContent);

            // Read the response from the API
            string responseContent = await response.Content.ReadAsStringAsync();
            return RedirectToPage("/ProductPage/Product");
        }
    }
}
