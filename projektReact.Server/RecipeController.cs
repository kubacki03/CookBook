using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projektReact.Server.DataModels;
using projektReact.Server.ProjektWPF.Data;
using projektReact.Server.RequestModels;

namespace projektReact.Server
{

    [Route("[controller]")]
    public class RecipeController : Controller
    {
        private readonly AppDbContext _context;

        public RecipeController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddRecipe(RecipeRequestModel model)
        {
            var username = User.Identity?.Name;
            var user=_context.Users.FirstOrDefault(l=>l.Username==username);
            if (user == null) { 
            return Unauthorized();
            }
            var recipe = new Recipe { Description = model.Description, Ingredients= (ICollection<Ingredient>)model.Ingredients, Title=model.Title, User=user, UserId=user.Id  };

            _context.Add(recipe);

            _context.SaveChanges();

            return Ok();
        }


        [Authorize]
        [HttpDelete]
        public IActionResult DeleteRecipe([FromQuery] long recipeId)
        {
            var username = User.Identity?.Name;
            var user = _context.Users.FirstOrDefault(l => l.Username == username);
            if (user == null )
            {
                return Unauthorized();
            }

           var recipe = _context.Recipes.FirstOrDefault(p => p.UserId == user.Id && p.Id==recipeId);

            if (recipe == null) { 
            return NotFound();
            }

            _context.Remove(recipe);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("GetRecipes")]
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

            return Ok(found);
        }



     

       

    }



}
