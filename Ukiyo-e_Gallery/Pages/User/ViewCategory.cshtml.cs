using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace Ukiyo_e_Gallery.Pages.User
{
    public class ViewCategoryModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewCategoryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        IEnumerable<Ukiyoe> Ukiyoes { get; set; }
        public void OnGet(string name)
        {
            Ukiyoes = _unitOfWork.Ukiyoe.GetAll(x => x.Artist.Name == name);
        }
    }
}
