using System.ComponentModel.DataAnnotations;

namespace LCS.BWA.DTOS
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public static implicit operator RegisterCommand(RegisterModel model)
        {
            return new RegisterCommand()
            {
                Email = model.Email,
                UserName = model.UserName,
                Password = model.Password,
            };
        }
    }
    public class RegisterCommand
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
    }
}
