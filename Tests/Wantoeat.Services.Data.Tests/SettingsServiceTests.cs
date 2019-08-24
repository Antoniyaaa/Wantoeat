﻿namespace Wantoeat.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Wantoeat.Data;
    using Wantoeat.Data.Common.Repositories;
    using Wantoeat.Data.Models;
    using Wantoeat.Data.Repositories;

    using Microsoft.EntityFrameworkCore;

    using Moq;

    using Xunit;

    public class SettingsServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            // Пример с мокнато репозитори
            var repository = new Mock<IDeletableEntityRepository<Setting>>();
            repository.Setup(r => r.All()).Returns(new List<Setting>
                                                        {
                                                            new Setting(),
                                                            new Setting(),
                                                            new Setting(),
                                                        }.AsQueryable());
            var service = new SettingsService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            // Тест с inmemory база данни
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Find_User_Database") // Give a Unique name to the DB
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Settings.Add(new Setting());
            dbContext.Settings.Add(new Setting());
            dbContext.Settings.Add(new Setting());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Setting>(dbContext);
            var service = new SettingsService(repository);
            var count = service.GetCount();
            Assert.Equal(3, count);
        }
    }
}