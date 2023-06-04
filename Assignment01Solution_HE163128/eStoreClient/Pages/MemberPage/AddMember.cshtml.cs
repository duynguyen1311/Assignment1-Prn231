using BusinessObjects.Models;
using eStoreClient.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace eStoreClient.Pages.MemberPage
{
    public class AddMemberModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string AddMemberApiUrl = "";
        private string MemberCateApiUrl = "";

        [BindProperty]
        public MemberAddRequest MemberAddRequest { get; set; }
        public AddMemberModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            AddMemberApiUrl = _configuration.GetValue<string>("DomainURL") + "Member/AddMember";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var request = new
            {
                CompanyName = MemberAddRequest.CompanyName,
                Email = MemberAddRequest.Email,
                City = MemberAddRequest.City,
                Country = MemberAddRequest.Country,
                Password = MemberAddRequest.Password
            };

            string body = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            // Make the POST request using HttpClient with the request body
            HttpResponseMessage response = await client.PostAsync(AddMemberApiUrl, httpContent);

            // Read the response from the API
            string responseContent = await response.Content.ReadAsStringAsync();

            return RedirectToPage("/MemberPage/Member");
        }
    }
}
