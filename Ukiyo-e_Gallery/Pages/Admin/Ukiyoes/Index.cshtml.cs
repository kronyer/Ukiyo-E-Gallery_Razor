using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Utility;

namespace Ukiyo_e_Gallery.Pages.Admin.Ukiyoes
{
    [Authorize(Roles = SD.AdministratorRole)]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) 
        {
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public IEnumerable<Ukiyoe> Ukiyoes { get; set; }

        public void OnGet()
        {
            Ukiyoes = _unitOfWork.Ukiyoe.GetAll(includeProperties: "Artist");
        }

        public IActionResult OnPostDelete(int id)
        {
            var image = _unitOfWork.Ukiyoe.GetFirstOrDefault(x => x.Id == id);
            if (image == null)
            {
                return NotFound();
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, image.Image.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            
            _unitOfWork.Ukiyoe.Remove(image);
            _unitOfWork.Save();
            return RedirectToPage("Index");
        }
    }
}
