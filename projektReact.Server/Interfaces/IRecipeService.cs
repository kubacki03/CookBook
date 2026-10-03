using projektReact.Server.RequestModels;

namespace projektReact.Server.Interfaces
{
    public interface IRecipeService
    { 
        Task<bool> AddRecipeAsync(string? username, RecipeRequest request, CancellationToken ct = default); 
        Task<List<RecipeDto>> SearchAsync(string name, CancellationToken ct = default); 
        Task<List<RecipeRequest>?> GetUserRecipesAsync(string? username, CancellationToken ct = default);
    }
}
