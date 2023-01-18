using Blazored.LocalStorage;
using LCS.BWA;
using LCS.BWA.AbstractServices;
using LCS.BWA.Authentication;
using LCS.BWA.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();

builder.Services.AddHttpClient<IUserService, UserService>(us =>
{
    us.BaseAddress = new Uri("https://localhost:7003/api/");
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();
