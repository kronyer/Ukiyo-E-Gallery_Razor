using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Utility;

namespace Ukiyo_e_Gallery.Pages.Admin.Artists
{
    [Authorize(Roles = SD.AdministratorRole)]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Artist Artist { get; set; }

        public void OnGet(int id)
        {
            Artist = _unitOfWork.Artist.GetFirstOrDefault(x => x.Id == id);
        }

        public async Task<IActionResult> OnPostAsync(int id) 
        {
        if (ModelState.IsValid)
            {
                _unitOfWork.Artist.Update(Artist);
                _unitOfWork.Save();
                TempData["success"] = "Artist updated";
                return RedirectToPage("Index");
            }
        return Page();
        
        }
    }
}
