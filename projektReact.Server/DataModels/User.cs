namespace projektReact.Server.DataModels
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
       
    }
}
