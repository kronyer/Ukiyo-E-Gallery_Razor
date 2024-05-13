using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace Ukiyo_e_Gallery.Pages.User
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Ukiyoe> Images { get; set; }
        public IEnumerable<Artist> Artists { get; set; }

        public void OnGet()
        {
            Images = _unitOfWork.Ukiyoe.GetAll(includeProperties: "Artist");
            Artists = _unitOfWork.Artist.GetAll();
        }
    }
}
