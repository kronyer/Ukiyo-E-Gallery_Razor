using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Utility;

namespace Ukiyo_e_Gallery.Pages.Admin.Ukiyoes
{
    [BindProperties]
    [Authorize(Roles = SD.AdministratorRole)]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public Ukiyoe Ukiyoe { get; set; }
        public IEnumerable<SelectListItem> ArtistList { get; set; }

        public void OnGet()
        {
            ArtistList = _unitOfWork.Artist.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }
        public async Task<IActionResult> OnPost()
        {
            string webPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            string fileName_New = Guid.NewGuid().ToString();
            var uploads = Path.Combine(webPath, @"images\Ukiyoes");
            var extension = Path.GetExtension(files[0].FileName);

            using (var fileStream = new FileStream(Path.Combine(uploads, fileName_New + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }
            Ukiyoe.Image = @"\images\Ukiyoes\" + fileName_New + extension;
            Ukiyoe.Posted = DateTime.Now;
            _unitOfWork.Ukiyoe.Add(Ukiyoe);
            _unitOfWork.Save();

            return RedirectToPage("/Admin/Ukiyoes/Index");
        }
    }
}
