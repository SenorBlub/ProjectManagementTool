using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectManagementTool.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string UserId { get; set; }

        public IActionResult OnPost()
        {
            if (Guid.TryParse(UserId, out Guid userId))
            {
                HttpContext.Session.SetString("UserId", userId.ToString());
                return RedirectToPage("/MainOverview");
            }
            else
            {
                ModelState.AddModelError("UserId", "Invalid GUID format");
                return Page();
            }
        }


        public void OnGet()
        {
        }
    }
}
