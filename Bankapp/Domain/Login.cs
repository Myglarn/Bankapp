namespace Bankapp.Domain
{
    /// <summary>
    /// Class and constructor for creating a log in user and pin
    /// </summary>
    public class Login
    {
        public string Username { get; set; }
        public string Pin { get; set; }        

        public Login(string username, string pin)
        {
            Username = username;
            Pin = pin;           
        }       
    }
}