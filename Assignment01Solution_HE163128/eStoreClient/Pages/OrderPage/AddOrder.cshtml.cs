using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using eStoreClient.Dto;

namespace eStoreClient.Pages.OrderPage
{
    public class AddOrderModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string AddOrderApiUrl = "";
        private string MemberApiUrl = "";
        private string ProductApiUrl = "";

        [BindProperty]
        public OrderAddRequest OrderAddRequest { get; set; }
        public List<Member> listMember { get; set; }
        public List<Product> listProduct { get; set; }
        public AddOrderModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            AddOrderApiUrl = _configuration.GetValue<string>("DomainURL") + "Order/AddOrder";
            MemberApiUrl = _configuration.GetValue<string>("DomainURL") + "Member/GetAllMember";
            ProductApiUrl = _configuration.GetValue<string>("DomainURL") + "Product/GetAllProduct";
        }

        public async Task<IActionResult> OnGetAsync()
        {
            HttpResponseMessage resp = await client.GetAsync(MemberApiUrl);

            var strData = await resp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            listMember = System.Text.Json.JsonSerializer.Deserialize<List<Member>>(strData, options);

            HttpResponseMessage resp1 = await client.GetAsync(ProductApiUrl);

            var strData1 = await resp1.Content.ReadAsStringAsync();
            var options1 = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            listProduct = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(strData1, options1);

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var request = new
            {
                MemberId = OrderAddRequest.MemberId,
                OrderDate = OrderAddRequest.OrderDate,
                RequireDate = OrderAddRequest.RequireDate,
                ShippedDate = OrderAddRequest.ShippedDate,
                Freight = OrderAddRequest.Freight,
                ProductId = OrderAddRequest.ProductId,
                UnitPrice = OrderAddRequest.UnitPrice,
                Quantity = OrderAddRequest.Quantity,
                Discount = OrderAddRequest.Discount,
            };

            string body = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            // Make the POST request using HttpClient with the request body
            HttpResponseMessage response = await client.PostAsync(AddOrderApiUrl, httpContent);

            // Read the response from the API
            string responseContent = await response.Content.ReadAsStringAsync();

            return RedirectToPage("/OrderPage/Order");
        }
    }
}
