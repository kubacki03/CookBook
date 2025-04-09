namespace projektReact.Server.DataModels
{
    public class Recipe
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
