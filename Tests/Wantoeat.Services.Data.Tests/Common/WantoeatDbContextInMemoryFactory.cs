namespace Wantoeat.Services.Data.Tests.Common
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Wantoeat.Data;

    public static class WantoeatDbContextInMemoryFactory
    {
        public static ApplicationDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            return new ApplicationDbContext(options);
        }
    }
}
