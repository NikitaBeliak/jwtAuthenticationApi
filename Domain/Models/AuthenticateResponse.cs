using Domain.Entitys;

namespace Domain.Models
{
    public class AuthenticateResponse
    {
        public int idUsers { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            idUsers = user.idUsers;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Token = token;
        }
    }
}
