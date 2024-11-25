/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;

namespace iWip.Client.Common.Helpers;

public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    private DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options, AuthenticationStateProvider authenticationStateProvider)
    {
        FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authenticationState.User;

        // Check if the user is authenticated and has at least one of the required permissions
        var _claim = user.FindFirst(c => c.Value == policyName);

        if (_claim == null)
        {
            return await FallbackPolicyProvider.GetDefaultPolicyAsync();
        }
        else if (user.Identity.IsAuthenticated)
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.RequireClaim(_claim.Type, policyName);
            return await Task.FromResult(policy.Build());
        }

        return await FallbackPolicyProvider.GetPolicyAsync(policyName);
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();
}