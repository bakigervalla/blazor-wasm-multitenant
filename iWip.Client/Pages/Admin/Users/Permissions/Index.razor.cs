/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Pages.Admin.Users.Components;
using iWip.Client.Pages.Admin.Users.Permissions.Components;
using iWip.Infrastructure.Models.Users;
using iWip.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace iWip.Client.Pages.Admin.Users.Permissions;

public partial class Index
{
    [Inject] private IUsersService UsersService { get; set; }

    private MudTable<Role> table;
    public IEnumerable<Role> Roles { get; set; }
    private bool _loading;

    protected override async Task OnInitializedAsync()
    {
        await LoadRolesAsync();
    }

    private async Task LoadRolesAsync()
    {
        _loading = true;

        Roles = await UsersService.GetRolesAsync();

        _loading = false;
    }

    private async Task SetRolePermissions(Role role)
    {
        var parameters = new DialogParameters();
        DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true };
        parameters.Add("Roles", Roles);
        parameters.Add("SelectedRole", role);
        DialogService.Show<_Permissions>(L["permissions"], parameters, options);
    }

    private async Task AddEditRole(bool IsEditMode, Role role = null)
    {
        var parameters = new DialogParameters();
        DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true };
        parameters.Add("Role", role);
        parameters.Add("IsEditMode", IsEditMode);
        var dialogresult = DialogService.Show<AddEditRoleDialog>(!IsEditMode ? L["roles_new"] : L["roles_edit"], parameters, options);
        var result = await dialogresult.Result;

        if (!result.Cancelled)
            await LoadRolesAsync();
    }

    private string? TenantText(Role row)
    {
        return !row.MANUFACTURER.HasValue || row.MANUFACTURER == 0 ? "iRobot" : row.Tenant.DisplayName;
    }
}
