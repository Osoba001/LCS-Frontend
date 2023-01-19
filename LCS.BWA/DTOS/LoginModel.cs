namespace LCS.BWA.DTOS
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public static implicit operator LoginCommand(LoginModel model)
        {
            return new LoginCommand
            {
                Email = model.Email,
                Password = model.Password
            };
        }
    }

    public class LoginCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
