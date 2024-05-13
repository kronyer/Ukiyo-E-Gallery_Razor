using DataAccess;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Utility;

namespace Ukiyo_e_Gallery.Pages.Admin.Artists
{
    [Authorize(Roles = SD.AdministratorRole)]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Artist Artist { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() { 
        if (ModelState.IsValid)
            {
                _unitOfWork.Artist.Add(Artist);
                _unitOfWork.Save();
                TempData["success"] = "Artist registered";

                return RedirectToPage("Index");

            }
            return Page();
        
        
        }
    }
}
