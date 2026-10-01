namespace projektReact.Server.DataModels
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = Roles.User;
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
       
    }

    public static class Roles
    {
        public const string User = "User";
        public const string Admin = "Admin";
    }
}
