namespace Wantoeat.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using Wantoeat.Data.Models;

    public interface ICategoryService
    {
        IQueryable<Category> All();

        Task<List<SelectListItem>> AllToSelectListItems();

    }
}
