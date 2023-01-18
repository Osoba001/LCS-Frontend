using Blazored.LocalStorage;
using LCS.BWA.AbstractServices;
using LCS.BWA.DTOS;
using LCS.BWA.DTOS.Results;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Json;


namespace LCS.BWA.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UserService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<ActionResult> AddRole(string name)
        {
            var resp = await _httpClient.PostAsync($"auth/add-user-role?userrole={name}",null);
            return await ReturnAction(resp);
        }

        public async Task<ActionResult> AddRoleToUser(Guid userId, string role)
        {
            var resp = await _httpClient.PutAsync($"auth/add-role-to-user?userid={userId},role={role}",null);
            return await ReturnAction(resp);
        }

        public async Task<ActionResult<UserQuery>> AllUsers()
        {
            var resp = await _httpClient.GetAsync("auth/all-users");
            return await ReturnAction<UserQuery>(resp);
        }

        public async Task<ActionResult> ChangePassword(ChangePasswordCommand changePasswordCommand)
        {
            var resp = await _httpClient.PutAsJsonAsync("auth/change-password", changePasswordCommand);
            return await ReturnAction(resp);
        }

        public async Task<ActionResult> FalseDeleteUser(Guid id)
        {
            var resp = await _httpClient.DeleteAsync($"auth/false-delete?userId={id}");
            return await ReturnAction(resp);
        }

        public async Task<ActionResult> ForgottenPassword(string email)
        {
            var resp = await _httpClient.PostAsync($"auth/forgot-password?email={email}", null);
            return await ReturnAction(resp);
        }

        public async Task<List<string>> GetAllRoles()
        {
            var resp= await _httpClient.GetStringAsync("auth/role");
            return JsonConvert.DeserializeObject<List<string>>(resp) ?? new List<string>();
        }

        public async Task<ActionResult<UserQuery>> GetFalseDeletedUsers()
        {
            var resp = await _httpClient.GetAsync("auth/False-deleted-users");
            return await ReturnAction<UserQuery> (resp);
        }

        public async Task<ActionResult> HardDeleteUser(Guid id)
        {
            var resp = await _httpClient.DeleteAsync($"auth/hard-delete?userId={id}");
            return await ReturnAction(resp);
        }

        public async Task<ActionResult> Login(LoginCommand loginCommand)
        {
            var resp = await _httpClient.PostAsJsonAsync("auth/login", loginCommand);
            await SaveToken(resp);
            return await ReturnAction(resp);
        }

        public async Task<ActionResult> NewPassword(NewPasswordCommand newPasswordCommand)
        {
            var resp = await _httpClient.PutAsJsonAsync("auth/new-password", newPasswordCommand);
            return await ReturnAction(resp);
        }

        public async Task<ActionResult> RecoverPassword(ConfirmRecoverPinCommand recoverPinCommand)
        {
            var resp = await _httpClient.PostAsJsonAsync("auth/confirm-password-recovery-pin", recoverPinCommand);
            return await ReturnAction(resp);
        }

        public async Task<ActionResult> Register(RegisterCommand registerCommand)
        {
            var resp = await _httpClient.PostAsJsonAsync("/auth", registerCommand);
            await SaveToken(resp);
            return await ReturnAction(resp);
        }

        public async Task<ActionResult> RemoveRole(string role)
        {
            var resp=await _httpClient.DeleteAsync($"auth/role?userRole={role}");
            return await ReturnAction(resp);
        }

        public async Task<ActionResult> RemoveRoleFromUser(Guid userId, string role)
        {
            var resp = await _httpClient.DeleteAsync($"auth/remove-role-from-user?userId={userId},role={role}");
            return await ReturnAction(resp);
        }

        public async Task<ActionResult> UndoFalseDelete(Guid id)
        {
            var res = await _httpClient.PutAsync($"auth/undo-false-delete?userId={id}",null);
            return await ReturnAction(res);
        }

        public async Task<ActionResult<UserQuery>> UsersByRoles(string role)
        {
            var res = await _httpClient.GetAsync($"auth/user-by-role?role={role}");
            return await ReturnAction<UserQuery>(res);  
        }

        private static async Task<ActionResult> ReturnAction(HttpResponseMessage resp)
        {
            if (resp.IsSuccessStatusCode)
                return new ActionResult();
            else
            {
                var res = new ActionResult();
                res.AddError(await resp.Content.ReadAsStringAsync());
                return res;
            }
        }
        private static async Task<ActionResult<T>> ReturnAction<T>(HttpResponseMessage resp) where T : class
        {
            var res = new ActionResult<T>();
            if (resp.IsSuccessStatusCode)
                res.Entities = await resp.Content.ReadFromJsonAsync<List<T>>() ?? new List<T>();
            else
                res.AddError(await resp.Content.ReadAsStringAsync());
            return res;
        }
        private async Task SaveToken(HttpResponseMessage resp)
        {
            if (resp.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync("token", resp.Content.ReadAsStringAsync());
                await _authenticationStateProvider.GetAuthenticationStateAsync();
            }
        }
    }
}
