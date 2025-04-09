namespace projektReact.Server.DataModels
{
    public class Ingredient
    {
        public long Id { get; set; }
        public long RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public string IngredientName { get; set; }
        public float Weight { get; set; }
    }
}
