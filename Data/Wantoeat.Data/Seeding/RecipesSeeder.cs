using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wantoeat.Data.Models;

namespace Wantoeat.Data.Seeding
{
    internal class RecipesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Recipes.Any())
            {
                return;
            }

            var soupsCategory = new Category { Name = "Soups" };
            var saladsCategory = new Category { Name = "Salads" };
            var pastaCategory = new Category { Name = "Pasta" };
            var meatCategory = new Category { Name = "Meat" };
            var vegetarianCategory = new Category { Name = "Vegetarian" };
            var veganCategory = new Category { Name = "Vegan" };
            var dessertsCategory = new Category { Name = "Desserts" };
            var drinksCategory = new Category { Name = "Drinks" };
            var fishCategory = new Category { Name = "Fish" };

            var categories = new List<Category>
            {
                soupsCategory, saladsCategory, pastaCategory, meatCategory, vegetarianCategory, veganCategory, dessertsCategory, drinksCategory, fishCategory
            };

            await dbContext.Categories.AddRangeAsync(categories);

            var cookingTime15 = new CookingTime { Name = "15 minutes" };
            var cookingTime30 = new CookingTime { Name = "30 minutes" };
            var cookingTime45 = new CookingTime { Name = "45 minutes" };
            var cookingTime1 = new CookingTime { Name = "1 hour" };
            var cookingTime1AndAHalf = new CookingTime { Name = "1.5 hours" };
            var cookingTime2 = new CookingTime { Name = "2 hours" };

            var cookingTimes = new List<CookingTime> { cookingTime15, cookingTime30, cookingTime45, cookingTime1, cookingTime1AndAHalf, cookingTime2 };

            await dbContext.CookingTimes.AddRangeAsync(cookingTimes);


            var gluten = new Allergen
            {
                Name = "Gluten",
                Description = "Wheat (such as spelt and Khorasan wheat/Kamut), rye, " +
    "barley and oats is often found in foods containing flour, such as some types of baking powder, batter, " +
    "breadcrumbs, bread, cakes, couscous, meat products, pasta, pastry, sauces, soups and fried foods which " +
    "are dusted with flour.",
                ImagePath = "/images/gluten.jpg"
            };
            var egg = new Allergen
            {
                Name = "Eggs",
                Description = "Eggs are often found in cakes, some meat products, mayonnaise, " +
            "mousses, pasta, quiche, sauces and pastries or foods brushed or glazed with egg.",
                ImagePath = "/images/eggs.jpg"
            };
            var fish = new Allergen
            {
                Name = "Fish",
                Description = "You will find this in some fish sauces, pizzas, relishes, " +
            "salad dressings, stock cubes and Worcestershire sauce.",
                ImagePath = "/images/fish.jpg"
            };
            var crustaceans = new Allergen
            {
                Name = "Crustaceans",
                Description = "Crabs, lobster, prawns and scampi are crustaceans. " +
            "Shrimp paste, often used in Thai and south-east Asian curries or salads, is an ingredient to look out for"
            ,
                ImagePath = "/images/crustaceans.jpg"
            };
            var celery = new Allergen
            {
                Name = "Celery",
                Description = "This includes celery stalks, leaves, seeds and the root " +
            "called celeriac. You can find celery in celery salt, salads, some meat products, soups and stock cubes"
            ,
                ImagePath = "/images/celery.jpg"
            };
            var lupin = new Allergen
            {
                Name = "Lupin",
                Description = "Yes, lupin is a flower, but it’s also found in flour! Lupin " +
            "flour and seeds can be used in some types of bread, pastries and even in pasta.",
                ImagePath = "/images/lupin.jpg"
            };
            var milk = new Allergen
            {
                Name = "Milk",
                Description = "Milk is a common ingredient in butter, cheese, cream, milk " +
            "powders and yoghurt. It can also be found in foods brushed or glazed with milk, and in powdered soups and " +
            "sauces.",
                ImagePath = "/images/milk.jpg"
            };
            var molluscs = new Allergen
            {
                Name = "Molluscs",
                Description = "These include mussels, land snails, squid and whelks, but can also be " +
            "commonly found in oyster sauce or as an ingredient in fish stews",
                ImagePath = "/images/molluscs.jpg"
            };
            var mustard = new Allergen
            {
                Name = "Mustard",
                Description = "Liquid mustard, mustard powder and mustard seeds fall into " +
            "this category. This ingredient can also be found in breads, curries, marinades, meat products, salad " +
            "dressings, sauces and soups.",
                ImagePath = "/images/mustard.jpg"
            };
            var nuts = new Allergen
            {
                Name = "Nuts",
                Description = "Not to be mistaken with peanuts (which are actually a legume " +
            "and grow underground), this ingredient refers to nuts which grow on trees, like cashew nuts, almonds and " +
            "hazelnuts. You can find nuts in breads, biscuits, crackers, desserts, nut powders (often used in Asian " +
            "curries), stir-fried dishes, ice cream, marzipan (almond paste), nut oils and sauces.",
                ImagePath = "/images/nuts.jpg"
            };
            var peanuts = new Allergen
            {
                Name = "Peanuts",
                Description = "Peanuts are actually a legume and grow underground, which " +
            "is why it’s sometimes called a groundnut. Peanuts are often used as an ingredient in biscuits, cakes, " +
            "curries, desserts, sauces (such as satay sauce), as well as in groundnut oil and peanut flour.",
                ImagePath = "/images/peanuts.jpg"
            };
            var sesame = new Allergen
            {
                Name = "Sesame seeds",
                Description = "These seeds can often be found in bread (sprinkled on" +
            " hamburger buns for example), breadsticks, houmous, sesame oil and tahini. They are sometimes toasted and" +
            " used in salads.",
                ImagePath = "/images/sesame.jpg"
            };
            var soya = new Allergen
            {
                Name = "Soya",
                Description = "Often found in bean curd, edamame beans, miso paste, textured " +
                "soya protein, soya flour or tofu, soya is a staple ingredient in oriental food. It can also be found in " +
                "desserts, ice cream, meat products, sauces and vegetarian products.",
                ImagePath = "/images/soya.jpg"
            };
            var sulphites = new Allergen
            {
                Name = "Sulphites",
                Description = "This is an ingredient often used in dried fruit such as " +
                "raisins, dried apricots and prunes. You might also find it in meat products, soft drinks, vegetables as " +
                "well as in wine and beer. If you have asthma, you have a higher risk of developing a reaction to sulphur " +
                "dioxide.",
                ImagePath = "/images/sulphites.jpg"
            };

            var allergens = new List<Allergen> { molluscs, gluten, egg, fish, crustaceans, celery, lupin, milk, mustard, nuts, peanuts, sesame, soya, sulphites };

            await dbContext.Allergens.AddRangeAsync(allergens);

            var pepper = new Ingredient
            {
                Name = "Black pepper",
                Description = "Black pepper is rich in a potent " +
                   "antioxidant called piperine, which may help prevent free radical damage to your cells. " +
                   "Black pepper has demonstrated cholesterol-lowering effects in rodent studies and is " +
                   "believed to boost the absorption of potential cholesterol-lowering supplements. Source:" +
                   " www.healthline.com. Image source: www.greenvalleyspices.com",
                ImagePath = "/images/Black pepper_56502315.jpg",
            };

            var oliveOil = new Ingredient
            {
                Name = "Olive oil",
                Description = "Olive oil is rich in monounsaturated oleic acid. This fatty acid is believed to have many beneficial effects and is a healthy choice for cooking. Extra virgin olive oil is loaded with antioxidants, some of which have powerful biological effects. Olive oil contains nutrients that fight inflammation. Source: www.healthline.com",
                ImagePath = "/images/Olive oil_10074387.jpg"
            };

            var salt = new Ingredient
            {
                Name = "Salt",
                Description = "Salt is mainly composed of two minerals, " +
                  "sodium and chloride, which have various functions in the body. It is found naturally in " +
                  "most foods, and is widely used to improve flavor. Limiting salt intake does result in a " +
                  "slight reduction in blood pressure. However, there is no strong evidence linking " +
                  "reduced intake to a lower risk of heart attacks, strokes or death. Source: www." +
                  "healthline.com",
                ImagePath = "/images/Salt_19222016.jpg",
            };

            var eggs = new Ingredient
            {
                Name = "Eggs",
                Description = "Eggs are a very good source of inexpensive, high quality protein. More than half the protein of an egg is found in the egg white along with vitamin B2 and lower amounts of fat than the yolk. Eggs are rich sources of selenium, vitamin D, B6, B12 and minerals minerals such as zinc, iron and copper. Source: www.bbcgoodfood.com.",
                ImagePath = "/images/Eggs_84302547.jpg",
                IngredientAllergens = new List<IngredientAllergen> { new IngredientAllergen { Allergen = egg } }
            };

            var aspargus = new Ingredient
            {
                Name = "Asparagus",
                Description = "Asparagus can be green, white or purple. Asparagus is high in fiber, folate and potassium. Asparagus contains bone-building vitamin K along with many antioxidants, including vitamins E, A and C. Each spear of asparagus has just 4 calories and contains no fat or cholesterol. Source: www.farmflavor.com",
                ImagePath = "/images/Asparagus_42150805.jpg"
            };

            var salmon = new Ingredient
            {
                Name = "Salmon",
                Description = "Salmon is an excellent source of high-quality protein, vitamins and minerals (including potassium, selenium and vitamin B12) but it is their content of omega-3 fatty acids that receives the most attention, and rightly so. It is this essential fat which is responsible for oily fish’s reputation as a valuable ‘brain food’. Source: www.bbcgoodfood.com",
                            ImagePath = "/images/Salmon_43977193.jpg",
                IngredientAllergens = new List<IngredientAllergen> { new IngredientAllergen { Allergen = fish } }
            };

            var butter = new Ingredient
            {
                Name = "Butter",
                Description = "Butter is produced by separating cream from the milk, then churning the cream to drain off the extra liquid. Butter contains significant amounts of calories and fat, packing over 100 calories and 11 grams of fat into 1 tablespoon (14 grams). Source: www.healthline.com",
                ImagePath = "/images/Butter_12459611.jpg",
                IngredientAllergens = new List<IngredientAllergen> { new IngredientAllergen { Allergen = milk } }
            };

            var mayo = new Ingredient
            {
                Name = "Mayonnaise",
                Description = "Mayonnaise is a thick, creamy sauce or dressing that is made of oil, egg yolks, lemon juice or vinegar, and seasonings. Since homemade mayonnaise is uncooked, be sure to use the freshest eggs possible, and ones that you are reasonably sure are free from salmonella. Homemade mayonnaise will last three to four days in the refrigerator. Source: www.recipes.howstuffworks.com",
                ImagePath = "/images/Mayonnaise_98591959.jpg",
                IngredientAllergens = new List<IngredientAllergen> { new IngredientAllergen { Allergen = egg } }
            };

            var lemon = new Ingredient
            {
                Name = "Lemon",
                Description = "Lemons contain approximately 10% carbs, which are mostly soluble fibers and simple sugars. Their main fiber is pectin, which may help lower blood sugar levels. Lemons are very rich in vitamin C. In addition, they’re a decent source of potassium and vitamin B6. Source: /www.healthline.com/",
                ImagePath = "/images/Lemon_5451301.jpg"
            };

            var avocado = new Ingredient
            {
                Name = "Avocado",
                Description = "It is loaded with healthy fats, fiber and various important nutrients. Avocados are very high in potassium, which should support healthy blood pressure levels. Avocados tend to be rich in fiber — about 7% by weight, which is very high compared to most other foods. Fiber may have important benefits for weight loss and metabolic health. Source: www.healthline.com",
                ImagePath = "/images/Avocado_27033664.jpg"
            };

            var potatoes = new Ingredient
            {
                Name = "Potatoes",
                Description = "Carbs are the main dietary component of potatoes. Those cooled down after boiling may provide some resistant starch, which can improve gut health. Potatoes are a good source of several vitamins and minerals, including potassium, folate, and vitamins C and B6. Source: www.healthline.com.",
                ImagePath = "/images/Potatoes_42474043.jpg"
            };

            var flour = new Ingredient
            {
                Name = "Flour",
                Description = "Description comming soon... Photo source: www.alibaba.com",
                ImagePath = "/images/Flour_7181840.jpg",
                IngredientAllergens = new List<IngredientAllergen> { new IngredientAllergen { Allergen = gluten } }
            };

            var yoghurt = new Ingredient
            {
                Name = "Yogurt",
                Description = "Description comming soon... Picture source: www.healthline.com",
                ImagePath = "/images/Yogurt_54402902.jpg",
                IngredientAllergens = new List<IngredientAllergen> { new IngredientAllergen { Allergen = milk } }
            };

            var pepperoni = new Ingredient
            {
                Name = "Pepperoni",
                Description = "Description comming soon... Picture source: www.thespruceeats.com",
                ImagePath = "/images/Pepperoni_65750235.jpg"
            };

            var souce = new Ingredient
            {
                Name = "Pizza Sauce",
                Description = "Description comming soon... Picture source: www.acouplecooks.com",
                ImagePath = "/images/Pizza Sauce_46767786.jpg"
            };

            var ingredients = new List<Ingredient> { pepper, oliveOil, salt, eggs, aspargus, salmon, butter, mayo, lemon, avocado, potatoes, flour, yoghurt, pepperoni, souce };

            await dbContext.Ingredients.AddRangeAsync(ingredients);

            var recipe1 = new Recipe
            {
                Name = "One Pan Lemon Garlic Salmon and Asparagus",
                Description = "Cut the " +
                 "fillet into 2-3 fillets, trim the asparagus, mince the garlic, squeeze the lemon and " +
                 "peel its zest. Heat a tablespoon of butter and a tablespoon of olive oil in a large pan. " +
                 "Wait until the pan is hot and the butter has fully melted. Add the salmon and asparagus, " +
                 "season with salt and pepper, and cook for about 3 - 4 minutes on one side. Flip and cook " +
                 "for about 3 - 4 minutes on the other side.Add the garlic and lemon zest.Cook the garlic " +
                 "for just 1 - 2 minutes or until it begins to brown.Turn off the heat and squeeze half a " +
                 "lemon into the dish. Source: www.gimmedelicious.com.",
                ImagePath = "/images/One pan_40850936.jpg",
                Category = fishCategory,
                CookingTime = cookingTime30,
                RecipeIngredient = new List<RecipeIngredient>
                    {
                        new RecipeIngredient { Ingredient = aspargus,
                            Quantity = "1 bunch" },
                        new RecipeIngredient { Ingredient = salmon,
                            Quantity = "0.5 kg" },
                        new RecipeIngredient { Ingredient = butter,
                            Quantity = "1 tablespoon" },
                         new RecipeIngredient { Ingredient = oliveOil,
                            Quantity = "1 tablespoon" },
                    },
                RecipeAllergens = new List<RecipeAllergen>
                    {
                        new RecipeAllergen { Allergen = fish },
                        new RecipeAllergen { Allergen = milk },
                    }
            };

            var recipe2 = new Recipe
            {
                Name = "Creamy Avocado Egg Salad",
                Description = "Pit and peel the avocado, boil hard the eggs and then peel and chop them. Mince parsley, dill or chives. In Medium bowl, add avocados and mash with a spoon until chunky. Add the remaining ingredients and mix with a spoon until creamy. Serve on whole-grain toast or enjoy with toasted pita chips." +
                "Source: www.gimmedelicious.com.",
                ImagePath = "/images/Creamy Avocado Egg Salad_9931526.jpg",
                Category = vegetarianCategory,
                CookingTime = cookingTime15,
                RecipeIngredient = new List<RecipeIngredient>
                    {
                        new RecipeIngredient { Ingredient = eggs,
                            Quantity = "3 pcs." },
                        new RecipeIngredient { Ingredient = mayo,
                            Quantity = "2 tablespoons" },
                        new RecipeIngredient { Ingredient = lemon,
                            Quantity = "1 teaspoon juice" },
                         new RecipeIngredient { Ingredient = avocado,
                            Quantity = "1 pcs." },
                    },
                RecipeAllergens = new List<RecipeAllergen>
                    {
                        new RecipeAllergen { Allergen = egg },
                    }
            };

            var recipe3 = new Recipe
            {
                Name = "Crispy Parmesan Crusted Chicken",
                Description = "Grate the parmesan cheese. Lay the chicken breasts out on a cutting board and cut in half horizontally. Generously season with Italian seasoning (or seasoning of choice), garlic powder, salt, and pepper; Set aside."+
                "Combine the parmesan cheese and almond flour in a medium shallow bowl.In another bowl," +
                "whisk the eggs.Dip the chicken breasts into the egg mixture then into the parmesan mixture.shake off the excess breading.Repeat until all the chicken cutlets are covered.Heat oil or butter in a large non - stick heavy duty pan.Add chicken cutlets in a single layer and cook for 5 - 6 minutes on each side, until golden and crispy.Be sure not to flip until the parmesan is golden on the first side or it will slide off.Repeat with remaining chicken cutlets."+
                "Source: www.gimmedelicious.com.",
                ImagePath = "/images/Crispy Parmesan Crusted Chicken_61277109.jpg",
                Category = meatCategory,
                CookingTime = cookingTime15,
                RecipeIngredient = new List<RecipeIngredient>
                    {
                        new RecipeIngredient { Ingredient = oliveOil,
                            Quantity = "3 tablespoons" },
                        new RecipeIngredient { Ingredient = salt, Quantity = "On taste"},
                        new RecipeIngredient { Ingredient = pepper, Quantity = "On taste"}
                    },
            };

            var recipe4 = new Recipe
            {
                Name = "Oven-Roasted Baby Potatoes",
                Description = "Gather the ingredients. Preheat oven to 400 F. In a large pot, cover baby potatoes with water and bring to a boil. Parboil, just until soft, about 8 to 10 minutes; then drain and cool immediately, and set aside." +
                "Slice cooled potatoes in half and gently toss with 1 / 4 cup olive oil, 1 / 4 teaspoon salt, and 1 / 2 teaspoon pepper, being sure to coat well.Place potatoes, cut - side down, on a non - stick baking pan and roast in the oven for 15 to 20 minutes, or until golden brown and crispy." +
                "Source: www.thespruceeats.com",
                ImagePath = "/images/Oven-Roasted Baby Potatoes_88906866.jpg",
                Category = veganCategory,
                CookingTime = cookingTime30,
                RecipeIngredient = new List<RecipeIngredient>
                    {
                        new RecipeIngredient { Ingredient = potatoes, 
                            Quantity = "0.5 kg" },
                        new RecipeIngredient { Ingredient = oliveOil,
                            Quantity = "3 spoons" },
                         new RecipeIngredient { Ingredient = salt,
                            Quantity = "on taste" },
                    },
            };

            var recipe5 = new Recipe
            {
                Name = "2-Ingredient Dough Pizza",
                Description = "Preheat oven to 200°C. In a large bowl, mix the self-rising flour and Greek yogurt until it comes together to form a ball. Transfer the dough ball to a lightly floured work surface and use your hands to begin flattening and shaping the rough into about a 12 - inch(30 cm) round.If at any point the dough begins to stick," +
                " sprinkle on a bit more flour. Carefully transfer the dough to a lightly floured baking sheet.Spread the sauce evenly across the dough, sprinkle on the cheese, and top with sliced pepperoni. Bake for 20 minutes, until the cheese has melted and the crust has turned golden - brown." +
                "Source: www.tasty.co",
                ImagePath = "/images/2-Ingredient Dough Pizza_17042355.jpg",
                Category = pastaCategory,
                CookingTime = cookingTime1,
                RecipeIngredient = new List<RecipeIngredient>
                    {
                        new RecipeIngredient { Ingredient = flour,
                            Quantity = "1.5 cups" },
                         new RecipeIngredient { Ingredient = yoghurt,
                            Quantity = "245 gr." },
                         new RecipeIngredient { Ingredient = pepperoni,
                            Quantity = "200 gr." },
                         new RecipeIngredient { Ingredient = souce,
                            Quantity = "130 gr." },
                    },
                RecipeAllergens = new List<RecipeAllergen>
                    {
                        new RecipeAllergen { Allergen = gluten },
                        new RecipeAllergen { Allergen = milk },
                    }
            };

            var recipes = new List<Recipe> { recipe1, recipe2, recipe3, recipe4, recipe5 };

            await dbContext.Recipes.AddRangeAsync(recipes);
        }
    }
}
