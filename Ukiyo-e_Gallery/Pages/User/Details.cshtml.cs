using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace Ukiyo_e_Gallery.Pages.User
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Ukiyoe Ukiyoe { get; set; }
        public void OnGet(int id)
        {
            Ukiyoe = _unitOfWork.Ukiyoe.GetFirstOrDefault(x => x.Id == id);
        }
    }
}
