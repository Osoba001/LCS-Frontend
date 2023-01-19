using LCS.BWA.DTOS;
using LCS.BWA.DTOS.Results;

namespace LCS.BWA.AbstractServices
{
    public interface IUserService
    {

        Task<ActionResult> Register(RegisterModel registerCommand);
        Task<ActionResult> Login(LoginModel loginCommand);
        Task<ActionResult> ForgottenPassword(string email);
        Task<ActionResult> RecoverPassword(ConfirmRecoverPinCommand recoverPinCommand);
        Task<ActionResult> NewPassword(NewPasswordCommand newPasswordCommand);
        Task<ActionResult> ChangePassword(ChangePasswordCommand changePasswordCommand);
        Task<ActionResult> HardDeleteUser(Guid id);
        Task<ActionResult> FalseDeleteUser(Guid id);
        Task<ActionResult<UserQuery>> UsersByRoles(string role);
        Task<ActionResult<UserQuery>> AllUsers();
        Task<ActionResult<UserQuery>> GetFalseDeletedUsers();
        Task<ActionResult> AddRoleToUser(Guid id, string role);
        Task<ActionResult> RemoveRoleFromUser(Guid id, string role);
        Task<ActionResult> UndoFalseDelete(Guid id);
        Task<ActionResult> AddRole(string name);
        Task<ActionResult> RemoveRole(string name);
        Task<List<string>> GetAllRoles();
    }
}
