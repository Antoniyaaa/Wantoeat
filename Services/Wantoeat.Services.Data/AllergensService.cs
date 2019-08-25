namespace Wantoeat.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    using Wantoeat.Data;
    using Wantoeat.Services.Mapping;

    public class AllergensService : IAllergensService
    {
        private readonly ApplicationDbContext dbContext;

        public AllergensService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<SelectListItem> AllToSelectListItems()
        {
            var allergens = this.dbContext.Allergens
                                .Select(x => new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name,
                                });

            return allergens;
        }

        public IQueryable<TViewModel> GetAllToViewModel<TViewModel>()
        {
            var allergens = this.dbContext.Allergens.To<TViewModel>();

            return allergens;
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id)
        {
            var allergen = await this.dbContext.Allergens
                .Where(i => i.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            return allergen;
        }

        public IQueryable<TViewModel> GetAllToViewModelByIds<TViewModel>(int[] ids)
        {
            var allergens = this.dbContext.Allergens
                .Where(x => ids.Contains(x.Id))
                .To<TViewModel>();

            return allergens;
        }
    }
}
