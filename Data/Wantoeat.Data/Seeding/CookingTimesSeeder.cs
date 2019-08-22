namespace Wantoeat.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Wantoeat.Data.Models;

    internal class CookingTimesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.CookingTimes.Any())
            {
                return;
            }

            IEnumerable<CookingTime> entities = new List<CookingTime>
            {
                new CookingTime { Name = "15 minutes"},
                new CookingTime { Name = "30 minutes"},
                new CookingTime { Name = "45 minutes"},
                new CookingTime { Name = "1 hour"},
                new CookingTime { Name = "1.5 hours"},
                new CookingTime { Name = "2 hours"},
            };

            await dbContext.CookingTimes.AddRangeAsync(entities);
        }
    }
}
