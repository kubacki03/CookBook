namespace projektReact.Server.RequestModels
{
    public class RecipeDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
    }
}
