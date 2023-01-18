using System.ComponentModel.DataAnnotations;

namespace LCS.BWA.DTOS
{
    public class RegisterCommand
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
