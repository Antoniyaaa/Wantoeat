namespace Wantoeat.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Wantoeat.Data.Models;

    internal class AllergensSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Allergens.Any())
            {
                return;
            }

            // Source:  https://food.gov.uk/
            IEnumerable<Allergen> entities = new List<Allergen>
            {
                new Allergen { Name = "Gluten", Description = "Wheat (such as spelt and Khorasan wheat/Kamut), rye, " +
                "barley and oats is often found in foods containing flour, such as some types of baking powder, batter, " +
                "breadcrumbs, bread, cakes, couscous, meat products, pasta, pastry, sauces, soups and fried foods which " +
                "are dusted with flour.", ImagePath = "/images/gluten.jpg" },
                new Allergen { Name = "Eggs", Description = "Eggs are often found in cakes, some meat products, mayonnaise, " +
                "mousses, pasta, quiche, sauces and pastries or foods brushed or glazed with egg.", ImagePath = "/images/eggs.jpg"},
                new Allergen { Name = "Fish", Description = "You will find this in some fish sauces, pizzas, relishes, " +
                "salad dressings, stock cubes and Worcestershire sauce.", ImagePath = "/images/fish.jpg"},
                new Allergen { Name = "Crustaceans", Description = "Crabs, lobster, prawns and scampi are crustaceans. " +
                "Shrimp paste, often used in Thai and south-east Asian curries or salads, is an ingredient to look out for"
                , ImagePath = "/images/crustaceans.jpg"},
                new Allergen { Name = "Celery", Description = "This includes celery stalks, leaves, seeds and the root " +
                "called celeriac. You can find celery in celery salt, salads, some meat products, soups and stock cubes"
                , ImagePath = "/images/celery.jpg"},
                new Allergen { Name = "Lupin", Description = "Yes, lupin is a flower, but it’s also found in flour! Lupin " +
                "flour and seeds can be used in some types of bread, pastries and even in pasta.", ImagePath = "/images/lupin.jpg"},
                new Allergen { Name = "Milk", Description = "Milk is a common ingredient in butter, cheese, cream, milk " +
                "powders and yoghurt. It can also be found in foods brushed or glazed with milk, and in powdered soups and " +
                "sauces.", ImagePath = "/images/milk.jpg"},
                new Allergen { Name = "Molluscs", Description = "These include mussels, land snails, squid and whelks, but can also be " +
                "commonly found in oyster sauce or as an ingredient in fish stews", ImagePath = "/images/molluscs.jpg"},
                new Allergen {Name = "Mustard", Description = "Liquid mustard, mustard powder and mustard seeds fall into " +
                "this category. This ingredient can also be found in breads, curries, marinades, meat products, salad " +
                "dressings, sauces and soups.", ImagePath = "/images/mustard.jpg"},
                new Allergen { Name = "Nuts", Description = "Not to be mistaken with peanuts (which are actually a legume " +
                "and grow underground), this ingredient refers to nuts which grow on trees, like cashew nuts, almonds and " +
                "hazelnuts. You can find nuts in breads, biscuits, crackers, desserts, nut powders (often used in Asian " +
                "curries), stir-fried dishes, ice cream, marzipan (almond paste), nut oils and sauces.", ImagePath = "/images/nuts.jpg"},
                new Allergen { Name = "Peanuts", Description = "Peanuts are actually a legume and grow underground, which " +
                "is why it’s sometimes called a groundnut. Peanuts are often used as an ingredient in biscuits, cakes, " +
                "curries, desserts, sauces (such as satay sauce), as well as in groundnut oil and peanut flour.",
                ImagePath = "/images/peanuts.jpg"},
                new Allergen { Name = "Sesame seeds", Description = "These seeds can often be found in bread (sprinkled on" +
                " hamburger buns for example), breadsticks, houmous, sesame oil and tahini. They are sometimes toasted and" +
                " used in salads.", ImagePath = "/images/sesame.jpg"},
                new Allergen { Name = "Soya", Description = "Often found in bean curd, edamame beans, miso paste, textured " +
                "soya protein, soya flour or tofu, soya is a staple ingredient in oriental food. It can also be found in " +
                "desserts, ice cream, meat products, sauces and vegetarian products.", ImagePath = "/images/soya.jpg"},
                new Allergen { Name = "Sulphites", Description = "This is an ingredient often used in dried fruit such as " +
                "raisins, dried apricots and prunes. You might also find it in meat products, soft drinks, vegetables as " +
                "well as in wine and beer. If you have asthma, you have a higher risk of developing a reaction to sulphur " +
                "dioxide.", ImagePath = "/images/sulphites.jpg"}
            };

            await dbContext.Allergens.AddRangeAsync(entities);
        }
    }
}
