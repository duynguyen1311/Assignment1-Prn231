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
    public class UpdateOrderModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string UpdateOrderApiUrl = "";
        private string UpdateOrderDetailApiUrl = "";
        private string OrderDetailApiUrl = "";
        private string OrderApiUrl = "";

        [BindProperty]
        public Order Order { get; set; }
        [BindProperty]
        public OrderDetail OrderDetail { get; set; }
        public List<Member> listMember { get; set; }
        public UpdateOrderModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UpdateOrderApiUrl = _configuration.GetValue<string>("DomainURL") + "Order/Update";
            OrderApiUrl = _configuration.GetValue<string>("DomainURL") + "Order/Detail/{id}";
            OrderDetailApiUrl = _configuration.GetValue<string>("DomainURL") + "OrderDetail/Detail/{id}";
            UpdateOrderDetailApiUrl = _configuration.GetValue<string>("DomainURL") + "OrderDetail/Update";
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            //Order
            string requestUrl = OrderApiUrl.Replace("{id}", id);
            HttpResponseMessage resp2 = await client.GetAsync(requestUrl);
            var strData2 = await resp2.Content.ReadAsStringAsync();
            var options2 = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Order = System.Text.Json.JsonSerializer.Deserialize<Order>(strData2, options2);

            //order detail
            string requestUrl1 = OrderDetailApiUrl.Replace("{id}", id);
            HttpResponseMessage resp3 = await client.GetAsync(requestUrl1);
            var strData3 = await resp3.Content.ReadAsStringAsync();
            var options3 = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            OrderDetail = System.Text.Json.JsonSerializer.Deserialize<OrderDetail>(strData3, options3);

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var request = new
            {
                OrderId = Order.OrderId,
                OrderDate = Order.OrderDate,
                RequireDate = Order.RequireDate,
                ShippedDate = Order.ShippedDate,
                Freight = Order.Freight,
                UnitPrice = OrderDetail.UnitPrice,
                Quantity = OrderDetail.Quantity,
                Discount = OrderDetail.Discount,
            };

            string body = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            // Make the POST request using HttpClient with the request body
            HttpResponseMessage response = await client.PutAsync(UpdateOrderApiUrl, httpContent);
            HttpResponseMessage response2 = await client.PutAsync(UpdateOrderDetailApiUrl, httpContent);

            return RedirectToPage("/OrderPage/Order");
        }
    }
}
