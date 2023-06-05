using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStoreClient.Pages.OrderPage
{
    public class OrderModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string OrderApiUrl = "";
        public List<Order> ListOrder { get; set; }

        [BindProperty]
        public string? Keyword { get; set; }

        public OrderModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OrderApiUrl = _configuration.GetValue<string>("DomainURL") + "Order/GetAllOrder";
        }
        public async Task<IActionResult> OnGetAsync(string? keyword)
        {

            Keyword = keyword;
            string url = OrderApiUrl + "?keyword=" + keyword;
            if (keyword == null)
            {
                url = OrderApiUrl;
            }
            HttpResponseMessage resp = await client.GetAsync(url);

            var strData = await resp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Order> listOrders = JsonSerializer.Deserialize<List<Order>>(strData, options);
            ListOrder = listOrders;
            return Page();
        }
    }
}
