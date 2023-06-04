using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace eStoreClient.Pages.MemberPage
{
    public class UpdateMemberModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string UpdateMemberApiUrl = "";
        private string MemberDetailApiUrl = "";

        [BindProperty]
        public Member Member { get; set; }

        public UpdateMemberModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            MemberDetailApiUrl = _configuration.GetValue<string>("DomainURL") + "Member/Detail/{id}";
            UpdateMemberApiUrl = _configuration.GetValue<string>("DomainURL") + "Member/Update";
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {

            string requestUrl = MemberDetailApiUrl.Replace("{id}", id);
            HttpResponseMessage res = await client.GetAsync(requestUrl);
            var strData1 = await res.Content.ReadAsStringAsync();
            var options1 = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Member = System.Text.Json.JsonSerializer.Deserialize<Member>(strData1, options1);

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var request = new
            {
                MemberId = Member.MemberId,
                CompanyName = Member.CompanyName,
                City = Member.City,
                Country = Member.Country,
                Email = Member.Email,
                Password = Member.Password
            };

            string body = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            // Make the POST request using HttpClient with the request body
            HttpResponseMessage response = await client.PutAsync(UpdateMemberApiUrl, httpContent);

            // Read the response from the API
            string responseContent = await response.Content.ReadAsStringAsync();
            return RedirectToPage("/MemberPage/Member");
        }
    }
}
