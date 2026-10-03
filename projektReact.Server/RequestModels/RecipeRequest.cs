namespace projektReact.Server.RequestModels
{
    public class RecipeRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<IngredientRequest> Ingredients { get; set; }
    }

    public sealed class IngredientRequest
    {
        public string IngredientName { get; set; }
        public float Weight { get; set; }
    }
}
