namespace projektReact.Server.RequestModels
{
    public class RecipeRequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<IngredientsModel> Ingredients { get; set; } = new List<IngredientsModel>();

    }


    public class IngredientsModel
    {
        public string Ingredient { get; set; }
        public float Weight { get; set; }
    }

}
