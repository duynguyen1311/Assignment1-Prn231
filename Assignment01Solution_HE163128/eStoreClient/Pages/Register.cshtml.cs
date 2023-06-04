using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStoreClient.Pages
{
    public class RegisterModel : PageModel
    {

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string CompanyName { get; set; }
        [BindProperty]
        public string City { get; set; }
        [BindProperty]
        public string Country { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public RegisterModel()
        {

        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                /*var result = await _signInManager
                    .PasswordSignInAsync(Register.Email, Register.Password, Register.RememberMe, lockoutOnFailure: false);
                var identityResult = await _userManager.FindByEmailAsync(Register.Email);
                if (!identityResult.Activated)
                {
                    ViewData["Title"] = "Account is not locked !";
                    return Page();
                }
                if (result.Succeeded) return RedirectToPage("/Index");
                else ViewData["Title"] = "Wrong password !";*/
            }
            else
            {
                ViewData["Title"] = "Incorrect email or password !";
            }
            return Page();
        }

    }
}
