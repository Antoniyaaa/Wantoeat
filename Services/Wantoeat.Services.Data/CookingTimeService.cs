namespace Wantoeat.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    using Wantoeat.Data;
    using Wantoeat.Data.Models;

    public class CookingTimeService : ICookingTimeService
    {
        private readonly ApplicationDbContext dbContext;

        public CookingTimeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<SelectListItem>> AllToSelectListItems()
        {
            var cookingTimes = await this.dbContext.CookingTimes
                                .Select(x => new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name,
                                })
                                .ToListAsync();

            return cookingTimes;
        }

        public IQueryable<CookingTime> GetAll()
        {
            return this.dbContext.CookingTimes;
        }
    }
}
