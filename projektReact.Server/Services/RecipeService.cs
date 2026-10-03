using Microsoft.EntityFrameworkCore;
using projektReact.Server.DataModels;
using projektReact.Server.Interfaces;
using projektReact.Server.ProjektWPF.Data;
using projektReact.Server.RequestModels;

namespace projektReact.Server.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly AppDbContext _context;

        public RecipeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRecipeAsync(string? username, RecipeRequest request, CancellationToken ct = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(l => l.Username == username, ct);
            if (user == null)
            {
                return false;
            }

            var recipe = new Recipe
            {
                Title = request.Title,
                Description = request.Description,
                UserId = user.Id
            };

            foreach (var ingredient in request.Ingredients)
            {
                recipe.Ingredients.Add(new Ingredient
                {
                    IngredientName = ingredient.IngredientName,
                    Weight = ingredient.Weight
                });
            }

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public Task<List<RecipeDto>> SearchAsync(string name, CancellationToken ct = default)
        {
            return _context.Recipes
                .Where(p => p.Title.Contains(name))
                .Select(recipe => new RecipeDto
                {
                    Id = recipe.Id,
                    Title = recipe.Title,
                    Description = recipe.Description,
                    Ingredients = recipe.Ingredients.Select(i => new IngredientDto
                    {
                        Id = i.Id,
                        IngredientName = i.IngredientName,
                        Weight = i.Weight
                    }).ToList()
                })
                .ToListAsync(ct);
        }

        public async Task<List<RecipeRequest>?> GetUserRecipesAsync(string? username, CancellationToken ct = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(l => l.Username == username, ct);
            if (user == null)
            {
                return null;
            }

            return await _context.Recipes
                .Where(p => p.UserId == user.Id)
                .Select(recipe => new RecipeRequest
                {
                    Title = recipe.Title,
                    Description = recipe.Description,
                    Ingredients = recipe.Ingredients.Select(i => new IngredientRequest
                    {
                        IngredientName = i.IngredientName,
                        Weight = i.Weight
                    }).ToList()
                })
                .ToListAsync(ct);
        }
    }
}
