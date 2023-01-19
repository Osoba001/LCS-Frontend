using LCS.BWA.AbstractServices;
using LCS.BWA.DTOS;
using Microsoft.AspNetCore.Components;

namespace LCS.BWA.Components.UserComponents
{
    public partial class Login
    {
        LoginModel loginCommand = new LoginModel();
        [Inject]
        private IUserService userService { get; set; }
        public bool HideFailureError { get; set; } = true;
        public string ErrorMessage { get; set; }
        public async Task SubmitLogin()
        {
            var res = await userService.Login(loginCommand);
            if (!res.IsSuccess)
            {
                ErrorMessage = res.FistError;
            }
        }
    }
}