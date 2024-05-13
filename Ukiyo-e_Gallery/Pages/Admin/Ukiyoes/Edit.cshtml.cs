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
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Ukiyoe Ukiyoes { get; set; }
        public IEnumerable<SelectListItem> ArtistList { get; set; }
        public void OnGet(int id)
        {
            Ukiyoes = _unitOfWork.Ukiyoe.GetFirstOrDefault(x => x.Id == id);
            ArtistList = _unitOfWork.Artist.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }

        public IActionResult OnPost()
        {
            Ukiyoes.Posted = DateTime.Now;
            _unitOfWork.Ukiyoe.Update(Ukiyoes);
            _unitOfWork.Save();
            TempData["success"] = "Ukiyoe updated";
            return RedirectToPage("/Admin/Ukiyoes/Index");


        }

    }
}
