namespace Bankapp.Domain
{
    public class Login
    {
        public string Username = "User";
        public int Pin = 1234;

        public Login(string username, int pin)
        {
            Username = username;
            Pin = pin;
        }
    }
}
