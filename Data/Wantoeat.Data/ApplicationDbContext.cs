namespace Wantoeat.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Wantoeat.Data.Common.Models;
    using Wantoeat.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Allergen> Allergens { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CookingTime> CookingTimes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<IngredientAllergen> IngredientAllergen { get; set; }

        public DbSet<ApplicationUserFavoriteRecipes> ApplicationUserFavoriteRecipes { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<RecipeAllergen> RecipeAllergen { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredient { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void ConfigureUserIdentityRelations(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // TODO - DeleteBehavior!!!!
            // Recipes & Ingredients
            builder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            builder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredient)
                .HasForeignKey(r => r.RecipeId);

            builder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(r => r.IngredientId);

            // Recipes & Allergens
            builder.Entity<RecipeAllergen>()
                .HasKey(ra => new { ra.RecipeId, ra.AllergenId });

            builder.Entity<RecipeAllergen>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeAllergens)
                .HasForeignKey(r => r.RecipeId);

            builder.Entity<RecipeAllergen>()
                .HasOne(ri => ri.Allergen)
                .WithMany(r => r.RecipeAllergens)
                .HasForeignKey(r => r.AllergenId);

            // Ingredients & Allergens
            builder.Entity<IngredientAllergen>()
                .HasKey(ia => new { ia.IngredientId, ia.AllergenId });

            builder.Entity<IngredientAllergen>()
                .HasOne(i => i.Ingredient)
                .WithMany(a => a.IngredientAllergens)
                .HasForeignKey(i => i.IngredientId);

            builder.Entity<IngredientAllergen>()
                .HasOne(a => a.Allergen)
                .WithMany(i => i.IngredientAllergens)
                .HasForeignKey(a => a.AllergenId);

            // Recipes & Users
            builder.Entity<ApplicationUserFavoriteRecipes>()
                .HasKey(ra => new { ra.ApplicationUserId, ra.RecipeId });

            builder.Entity<ApplicationUserFavoriteRecipes>()
                .HasOne(ri => ri.ApplicationUser)
                .WithMany(r => r.FavouriteRecipes)
                .HasForeignKey(r => r.ApplicationUserId);

            builder.Entity<ApplicationUserFavoriteRecipes>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.FavouriteRecipes)
                .HasForeignKey(r => r.RecipeId);



            // one to many
            builder.Entity<Recipe>()
                .HasOne<Category>(r => r.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CategoryId);

            builder.Entity<Recipe>()
                .HasOne<CookingTime>(r => r.CookingTime)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CookingTimeId);

            builder.Entity<Comment>()
                .HasOne<ApplicationUser>(r => r.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(r => r.UserId);

            builder.Entity<Comment>()
                .HasOne<Recipe>(r => r.Recipe)
                .WithMany(c => c.Comments)
                .HasForeignKey(r => r.RecipeId);
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
