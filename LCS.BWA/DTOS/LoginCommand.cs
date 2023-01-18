namespace LCS.BWA.DTOS
{
    public class LoginCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
