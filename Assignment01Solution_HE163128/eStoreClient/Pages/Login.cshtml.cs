using BusinessObjects.Models;
using eStoreClient.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace eStoreClient.Pages
{
    public class LoginModel : PageModel
    {

        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string LoginApiUrl = "";

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            LoginApiUrl = _configuration.GetValue<string>("DomainURL") + "Member/Login";
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var request = new
            {
                Email = Email,
                Password = Password
            };

            string body = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            // Make the POST request using HttpClient with the request body
            HttpResponseMessage response = await client.PostAsync(LoginApiUrl, httpContent);

            // Read the response from the API
            string responseContent = await response.Content.ReadAsStringAsync();
            
            if (responseContent == "true")
            {
                ViewData["Login"] = "Login Success";
                return RedirectToPage("/Index");
            }
            else
            {
                ViewData["Title"] = "Incorrect email or password !";
            }
            return Page();
        }

    }
}
