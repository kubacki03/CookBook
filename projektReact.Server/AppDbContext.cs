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
                    .HasMany(a=>a.Ingredients)
                    .WithOne(p=>p.Recipe)
                    .HasForeignKey(d=>d.RecipeId);

                var us = new User
                {
                    Id = "sdadsaookl",
                    Username = "admin@wp.pl",
                    Password = "AQAAAAIAAYagAAAAEFacXzmHLjAGh4bGa5X2lO32T424kyKeGpsAVvSYAYr529ULQQ7tUrVJ9E/bFTR7mA=="
                };
                modelBuilder.Entity<User>().HasData(us);

            }
        }
    }
}
