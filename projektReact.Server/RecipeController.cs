using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projektReact.Server.DataModels;
using projektReact.Server.ProjektWPF.Data;
using projektReact.Server.RequestModels;

namespace projektReact.Server
{
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



            return Ok();
        }


        [Authorize]
        [HttpDelete]
        public IActionResult DeleteRecipe(long recipeId)
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

    }
}
