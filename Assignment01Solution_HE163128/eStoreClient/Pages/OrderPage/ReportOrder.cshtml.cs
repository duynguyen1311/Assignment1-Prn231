using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace eStoreClient.Pages.ReportOrderPage
{
    public class ReportOrderModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string ReportOrderApiUrl = "";
        public List<OrderDetail> ListReportOrder { get; set; }

        [BindProperty]
        public string? FromDate { get; set; }
        [BindProperty]
        public string? ToDate { get; set; }

        public ReportOrderModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ReportOrderApiUrl = _configuration.GetValue<string>("DomainURL") + "OrderDetail/Report";
        }
        public async Task<IActionResult> OnGetAsync(string? fromDate, string? toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;


            string url = ReportOrderApiUrl + "?fromDate=" + fromDate + "&toDate=" + toDate;
            if (fromDate == null && toDate == null)
            {
                url = ReportOrderApiUrl;
            }
            HttpResponseMessage resp = await client.GetAsync(url);

            var strData = await resp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<OrderDetail> listReportOrders = JsonSerializer.Deserialize<List<OrderDetail>>(strData, options);
            ListReportOrder = listReportOrders;
            return Page();
        }
    }
}
