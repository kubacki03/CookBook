using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projektReact.Server.Interfaces;
using projektReact.Server.RequestModels;

namespace projektReact.Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost("AddRecipe")]
        public async Task<IActionResult> AddRecipe([FromBody] RecipeRequest request, CancellationToken ct)
        {
            var added = await _recipeService.AddRecipeAsync(User.Identity?.Name, request, ct);
            if (!added)
            {
                return Unauthorized();
            }

            return Ok(new { message = "Przepis dodany pomyślnie!" });
        }

        [HttpGet("GetRecipes")]
        public async Task<IActionResult> GetRecipes([FromQuery] string name = "", CancellationToken ct = default)
        {
            var found = await _recipeService.SearchAsync(name, ct);
            return Ok(found);
        }

        [HttpGet("GetUserRecipes")]
        public async Task<IActionResult> GetUserRecipes(CancellationToken ct)
        {
            var recipes = await _recipeService.GetUserRecipesAsync(User.Identity?.Name, ct);
            if (recipes == null)
            {
                return Unauthorized();
            }

            return Ok(recipes);
        }
    }
}
