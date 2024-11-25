/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Services.Auth;
using iWip.Infrastructure.Models.Users;
using iWip.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;

namespace iWip.Client.Components.Shared;

public partial class UserMenu
{
    [Inject] AuthenticationStateProvider authStateProvider { get; set; }
    [Inject] IAuthService authService { get; set; }

    [Parameter] public string Class { get; set; }
    [Parameter] public User User { get; set; }

    public async Task Logout()
    {
        var response = await authService.LogOutAsync();

        if (response.HttpStatusCode == HttpStatusCode.OK)
        {
            await ((AuthStateProvider)authStateProvider).UpdateAuthenticationState(authService.User);
            NavigationManager.NavigateTo("/authentication/login");
        }
    }
}