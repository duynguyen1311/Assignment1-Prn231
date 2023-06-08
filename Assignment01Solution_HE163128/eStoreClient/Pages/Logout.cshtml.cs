using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStoreClient.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            HttpContext.Session.Remove("IsLoggedIn");
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }
}
