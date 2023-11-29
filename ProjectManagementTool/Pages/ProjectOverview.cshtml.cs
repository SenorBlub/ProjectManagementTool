using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectManagementTool.Pages
{
    public class ProjectOverviewModel : PageModel
    {
        public string projectId { get; private set; }
        public void OnGet(string id)
        {
            projectId = id;

        }
    }
}
