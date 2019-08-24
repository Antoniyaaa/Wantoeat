namespace Wantoeat.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IAllergensService
    {
        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id);

        IQueryable<SelectListItem> AllToSelectListItems();

        IQueryable<TViewModel> GetAllToViewModel<TViewModel>();

        IQueryable<TViewModel> GetAllToViewModelByIds<TViewModel>(int[] ids);
    }
}
