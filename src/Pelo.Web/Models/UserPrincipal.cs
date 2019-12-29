namespace Pelo.Web.Models
{
    public class UserPrincipal
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string Username { get; set; }

        public void Update(string id,
                           string username,
                           string displayName)
        {
            int i;
            if(int.TryParse(id,
                            out i))
            {
                Id = i;
                Username = username;
                DisplayName = displayName;
            }
        }
    }
}
