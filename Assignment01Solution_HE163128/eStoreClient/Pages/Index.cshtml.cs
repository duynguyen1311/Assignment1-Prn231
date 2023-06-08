using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStoreClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToPage("/Login"); // Redirect to the login page if not logged in
            }
            else
            {
                return Page();
            }
        }
        private bool IsUserLoggedIn()
        {
            // Replace this logic with your own authentication check
            // For example, you can check if a session variable indicating login status exists
            return HttpContext.Session.GetString("IsLoggedIn") == "true";
        }
    }
}