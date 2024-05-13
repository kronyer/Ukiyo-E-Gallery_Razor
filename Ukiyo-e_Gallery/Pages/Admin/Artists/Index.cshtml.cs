using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Utility;

namespace Ukiyo_e_Gallery.Pages.Admin.Artists
{

    [Authorize(Roles = SD.AdministratorRole)]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IEnumerable<Artist> Artists { get; set; }
        
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            Artists = _unitOfWork.Artist.GetAll();
        }

        public IActionResult OnPostDelete(int id)
        {
            var artist = _unitOfWork.Artist.GetFirstOrDefault(x => x.Id == id);
            if (artist == null)
            {
                return NotFound();
            }
            _unitOfWork.Artist.Remove(artist);
            _unitOfWork.Save();
            return RedirectToPage("Index");
        }
    }
}
