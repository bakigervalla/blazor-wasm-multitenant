/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Common.Extensions;
using iWip.Infrastructure.Models.Users;
using iWip.Infrastructure.Services.Auth;
using iWip.Infrastructure.Services.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;

namespace iWip.Client.Services.Auth;

public class AuthStateProvider : AuthenticationStateProvider
{
    //Constructor with instances.
    private ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    private readonly ILocalStorageService localStorageService;
    private readonly IAuthService _authService;

    public AuthStateProvider(ILocalStorageService localStorageService, IAuthService authService)
    {
        this.localStorageService = localStorageService;
        _authService = authService;
    }


    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorageService.GetItem<string>("iwipcloud_token");

        if (string.IsNullOrEmpty(token)) return await Task.FromResult(new AuthenticationState(anonymous));

        var (expireDate, username, sub) = await token.GetClaimsFromJWT();

        if (expireDate <= DateTime.UtcNow)
            return await Task.FromResult(new AuthenticationState(anonymous));

        User _user;
        if (_authService.User == null)
        {
            _user = DeSerializedUserSession(await localStorageService.GetItem<string>("iwipcloud_user"));
            _authService.SetUserFromLocalStorage(_user);
        }
        else
            _user = _authService.User;

        var principal = SetClaimsPrincipal(_user);

        return new AuthenticationState(principal);
    }

    public async Task UpdateAuthenticationState(User userSession)
    {
        if (userSession is not null && !string.IsNullOrEmpty(userSession.AccessToken))
        {
            await localStorageService.SetItem("iwipcloud_token", userSession.AccessToken);
            await localStorageService.SetItem("iwipcloud_user", SerializedUserSession(userSession));

            var principal = SetClaimsPrincipal(userSession);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }
        else
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }
    }

    private ClaimsPrincipal SetClaimsPrincipal(User userSession)
    {
        IEnumerable<Claim> claims;

        if (userSession.Claims != null)
            claims = new[] {
                new Claim(ClaimTypes.Name, userSession.Email),
                // new Claim(ClaimTypes.Role, string.Join(",", userSession.Roles.Select(x => x.Name).ToArray()))
            }
            .Concat(userSession.Roles.Select(x => new Claim(ClaimTypes.Role, x.Name)))
            .Concat(userSession.Claims.Select(c => new Claim(c.Key, c.Value)));
        else
            claims = new[] {
                new Claim(ClaimTypes.Name, userSession.Email),
                // new Claim(ClaimTypes.Role, string.Join(",", userSession.Roles.Select(x => x.Name).ToArray()))
            }.Concat(userSession.Roles.Select(x => new Claim(ClaimTypes.Role, x.Name)));

        return new ClaimsPrincipal(new ClaimsIdentity(claims, "JwtAuth"));
    }

    private string SerializedUserSession(User userSession)
    {
        return JsonConvert.SerializeObject(userSession);
    }
    private User DeSerializedUserSession(string serialisedString)
    {
        return JsonConvert.DeserializeObject<User>(serialisedString)!;
    }
}