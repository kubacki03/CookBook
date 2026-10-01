namespace projektReact.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Emit;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using projektReact.Server.DataModels;
    using projektReact.Server.RequestModels;

    namespace ProjektWPF.Data
    {
        public class AppDbContext : DbContext
        {
            public DbSet<User> Users { get; set; }

            public DbSet<Recipe> Recipes { get; set; }

            public DbSet<Ingredient> Ingredients { get; set; }
            public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
            {
            }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>()
                     .HasMany(p => p.Recipes)
                     .WithOne(s => s.User)
                     .HasForeignKey(l => l.UserId);

                modelBuilder.Entity<Recipe>()
                    .HasMany(a => a.Ingredients)
                    .WithOne(p => p.Recipe)
                    .HasForeignKey(d => d.RecipeId);

                var us = new User
                {
                    Id = "sdadsaookl",
                    Username = "admin@wp.pl",
                    Password = "AQAAAAIAAYagAAAAEFacXzmHLjAGh4bGa5X2lO32T424kyKeGpsAVvSYAYr529ULQQ7tUrVJ9E/bFTR7mA==",
                    Role = Roles.Admin
                };
                modelBuilder.Entity<User>().HasData(us);

                modelBuilder.Entity<Recipe>().HasData(
                    new Recipe { Id = 1, Title = "Spaghetti Bolognese", Description = "Classic Italian pasta dish with meat sauce." },
                    new Recipe { Id = 2, Title = "Pancakes", Description = "Fluffy pancakes perfect for breakfast." },
                    new Recipe { Id = 3, Title = "Chicken Curry", Description = "Spicy and savory Indian-style curry." },
                    new Recipe { Id = 4, Title = "Greek Salad", Description = "Fresh salad with feta cheese and olives." },
                    new Recipe { Id = 5, Title = "Chocolate Cake", Description = "Rich and moist chocolate dessert." }
                );

                modelBuilder.Entity<Ingredient>().HasData(
                   // Spaghetti Bolognese
                   new Ingredient { Id = 1, RecipeId = 1, IngredientName = "Spaghetti", Weight = 200 },
                   new Ingredient { Id = 2, RecipeId = 1, IngredientName = "Ground Beef", Weight = 300 },
                   new Ingredient { Id = 3, RecipeId = 1, IngredientName = "Tomato Sauce", Weight = 150 },
                   new Ingredient { Id = 4, RecipeId = 1, IngredientName = "Onion", Weight = 50 },
                   new Ingredient { Id = 5, RecipeId = 1, IngredientName = "Garlic", Weight = 10 },

                   // Pancakes
                   new Ingredient { Id = 6, RecipeId = 2, IngredientName = "Flour", Weight = 200 },
                   new Ingredient { Id = 7, RecipeId = 2, IngredientName = "Milk", Weight = 300 },
                   new Ingredient { Id = 8, RecipeId = 2, IngredientName = "Eggs", Weight = 100 },
                   new Ingredient { Id = 9, RecipeId = 2, IngredientName = "Sugar", Weight = 30 },
                   new Ingredient { Id = 10, RecipeId = 2, IngredientName = "Baking Powder", Weight = 10 },

                   // Chicken Curry
                   new Ingredient { Id = 11, RecipeId = 3, IngredientName = "Chicken Breast", Weight = 400 },
                   new Ingredient { Id = 12, RecipeId = 3, IngredientName = "Coconut Milk", Weight = 200 },
                   new Ingredient { Id = 13, RecipeId = 3, IngredientName = "Curry Powder", Weight = 15 },
                   new Ingredient { Id = 14, RecipeId = 3, IngredientName = "Onion", Weight = 100 },
                   new Ingredient { Id = 15, RecipeId = 3, IngredientName = "Garlic", Weight = 20 },

                   // Greek Salad
                   new Ingredient { Id = 16, RecipeId = 4, IngredientName = "Cucumber", Weight = 150 },
                   new Ingredient { Id = 17, RecipeId = 4, IngredientName = "Tomatoes", Weight = 200 },
                   new Ingredient { Id = 18, RecipeId = 4, IngredientName = "Feta Cheese", Weight = 100 },
                   new Ingredient { Id = 19, RecipeId = 4, IngredientName = "Black Olives", Weight = 80 },
                   new Ingredient { Id = 20, RecipeId = 4, IngredientName = "Red Onion", Weight = 50 },

                   // Chocolate Cake
                   new Ingredient { Id = 21, RecipeId = 5, IngredientName = "Flour", Weight = 250 },
                   new Ingredient { Id = 22, RecipeId = 5, IngredientName = "Cocoa Powder", Weight = 50 },
                   new Ingredient { Id = 23, RecipeId = 5, IngredientName = "Sugar", Weight = 200 },
                   new Ingredient { Id = 24, RecipeId = 5, IngredientName = "Eggs", Weight = 100 },
                   new Ingredient { Id = 25, RecipeId = 5, IngredientName = "Butter", Weight = 100 }
               );
            }
        }
    }
}
