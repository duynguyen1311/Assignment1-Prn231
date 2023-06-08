using BusinessObjects.Models;
using eStoreClient.Dto;
using eStoreClient.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStoreClient.Pages
{
    public class UserProfileModel : PageModel
    {

        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string ProfileApiUrl = "";

        [BindProperty]
        public UserProfileViewModel Profile { get; set; }

        [BindProperty]
        public ChangePasswordRequest Request { get; set; }
        public UserProfileModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProfileApiUrl = _configuration.GetValue<string>("DomainURL") + "Member/Detail/{id}";
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            string requestUrl = ProfileApiUrl.Replace("{id}", id);
            HttpResponseMessage res = await client.GetAsync(requestUrl);
            var strData1 = await res.Content.ReadAsStringAsync();
            var options1 = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var member = System.Text.Json.JsonSerializer.Deserialize<Member>(strData1, options1);
            Profile.ProfileId = member.MemberId;
            Profile.Email = member.Email;
            Profile.CompanyName = member.CompanyName;
            Profile.City = member.City;
            Profile.Country = member.Country;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
               /* var user = await _userManager.FindByIdAsync(Profile.Id);
                var role = await _userManager.GetRolesAsync(user);
                if ((role.Contains(RoleConstant.ADMIN)) || (role.Contains(RoleConstant.TEACHER)))
                {
                    _mapper.Map(Profile, user);
                    await _userManager.UpdateAsync(user);
                    return Page();
                }
                var student = await _studentRepository.GetById(Guid.Parse(Profile.Id));
                Profile.StudentCode = student.StudentCode;
                _mapper.Map(Profile, user);
                _mapper.Map(Profile, student);
                await _userManager.UpdateAsync(user);
                await _studentRepository.Update(student);*/
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("Error");
            }
        }
        /*public async Task<IActionResult> OnPostChangePassword()
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Profile.Id);
                var checkOldPass = await _userManager.CheckPasswordAsync(user, Request.OldPassword);
                if (checkOldPass)
                {
                    if (Request.NewPassword == Request.ConfirmPassword)
                    {
                        var result = await _userManager.ChangePasswordAsync(user, Request.OldPassword, Request.NewPassword);
                        if (result.Succeeded)
                        {
                            TempData["SuccessMessage"] = "Change password successfully";
                            return Page();
                        }
                        TempData["ErrorMessage"] = result.Errors.FirstOrDefault().Description;
                        return Page();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "New password and confirm password are not the same";
                        return Page();
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Old password is not correct";
                    return Page();
                }


            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("Error");
            }
        }*/

    }
}
