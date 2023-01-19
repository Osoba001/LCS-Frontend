using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using LCS.BWA;
using LCS.BWA.Shared;
using LCS.BWA.DTOS;
using LCS.BWA.AbstractServices;

namespace LCS.BWA.Components.UserComponents
{
    public partial class Registration
    {
        RegisterModel newUser = new();
        [Inject]
        private IUserService userService { get; set; }
        [CascadingParameter(Name ="ErrorComponent")]
        protected IErrorInfo errorInfo { get; set; }
         async void InsertUser()
        {
            var res = await userService.Register(newUser);
            if (!res.IsSuccess)
            {
                errorInfo.ShowError("Signup fail!", res.FistError);
            }
        }
    }
}