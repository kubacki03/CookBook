using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projektReact.Server.DataModels;
using projektReact.Server.ProjektWPF.Data;
using projektReact.Server.RequestModels;

namespace projektReact.Server
{

    [Route("[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly AppDbContext _context;

        public RecipeController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpPost("AddRecipe")]
        public IActionResult AddRecipe([FromBody] RecipeRequest request)
        {
            var username = User.Identity?.Name;
            var user = _context.Users.FirstOrDefault(l => l.Username == username);
            if (user == null)
            {
                return Unauthorized();
            }

            var recipe = new Recipe();

            recipe.Title = request.Title;
            recipe.Description = request.Description;
            recipe.User = user;
            recipe.UserId = user.Id;

            foreach(var ingredient in request.Ingredients){
                recipe.Ingredients.Add(new Ingredient { IngredientName = ingredient.IngredientName, Weight = ingredient.Weight });
            }

            _context.Add(recipe);
            _context.SaveChanges();
            return Ok(new { message = "Przepis dodany pomyślnie!" });
        }


        [HttpGet("GetRecipes")]
      [Authorize]
        public IActionResult GetRecipes([FromQuery] string name)
        {
            var found = _context.Recipes
                .Include(r => r.Ingredients)
                .Where(p => p.Title.Contains(name))
                .Select(recipe => new RecipeDto
                {
                    Id = recipe.Id,
                    Title = recipe.Title,
                    Description = recipe.Description,
                    Ingredients = recipe.Ingredients.Select(i => new IngredientDto
                    {
                        Id = recipe.Id,
                        IngredientName = i.IngredientName,
                        Weight = i.Weight
                    }).ToList()
                })
                .ToList();
            Console.Write("User "+User.Identity?.Name);
            return Ok(found);
        }


        [HttpGet("GetUserRecipes")]
        [Authorize]
        public IActionResult GetUserRecipes()
        {
            var username = User.Identity?.Name;
            var user = _context.Users.FirstOrDefault(l => l.Username == username);
            if (user == null)
            {
                return Unauthorized();
            }
            var recipes = _context.Recipes.Include(x=>x.Ingredients).Where(p => p.UserId == user.Id);

            var recipesDtoList = new List<RecipeRequest>();

            foreach(var recipe in recipes)
            {
                recipesDtoList.Add(new RecipeRequest
                {
                    
                    Title = recipe.Title,
                    Description = recipe.Description,
                    Ingredients = recipe.Ingredients.Select(i => new IngredientRequest
                    {
                        IngredientName = i.IngredientName,
                        Weight = i.Weight
                    }).ToList()
                });

            }

            return Ok(recipesDtoList);
        }
 
    }


    public class RecipeRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<IngredientRequest> Ingredients { get; set; }
    }

    public class IngredientRequest
    {
        public string IngredientName { get; set; }
        public float Weight { get; set; }
    }


}
