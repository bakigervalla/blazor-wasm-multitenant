/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Infrastructure.Models.Users;

namespace iWip.Client;

public static class PermissionsHelper
{
    public static bool HasAccess(this User user, string[] permissions)
    {
        try
        {
            if (user.Claims == null || !user.Claims.Any() || permissions == null)
                return false;

            if (permissions.Contains("*"))
                return true;

            return permissions.Any(permission => user.Claims.ContainsValue(permission));
        }
        catch { return false; }
    }

    public static bool IsInRole(this User user, string[] roles)
    {
        try
        {
            if (user.Claims == null || !user.Claims.Any() || roles == null)
                return false;

            var userRoles = user.Claims.Select(claim => claim.Value.Split(',')).SelectMany(x => x);

            return roles.Any(role => userRoles.Contains(role.Trim()));
        }
        catch { return false; }
    }

}
