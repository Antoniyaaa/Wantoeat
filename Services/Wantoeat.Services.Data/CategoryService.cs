namespace Wantoeat.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    using Wantoeat.Data;
    using Wantoeat.Data.Models;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Category> GetAll()
        {
            var categories = this.dbContext.Categories;

            return categories;
        }

        public async Task<List<SelectListItem>> AllToSelectListItems()
        {
            var categories = await this.dbContext.Categories
                                .Select(x => new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name,
                                })
                                .ToListAsync();

            return categories;
        }
    }
}
