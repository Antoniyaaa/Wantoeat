namespace Wantoeat.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICookingTimeService
    {
        Task<List<SelectListItem>> AllToSelectListItems();
    }
}
