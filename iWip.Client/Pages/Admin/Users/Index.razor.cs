/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Pages.Admin.Users.Components;
using iWip.Infrastructure.Common.Pagination;
using iWip.Infrastructure.Models.Users;
using iWip.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace iWip.Client.Pages.Admin.Users;

public partial class Index
{
    private MudTable<User> table;

    [Inject] private IUsersService UsersService { get; set; }

    public IList<User> Users { get; set; } = new List<User>();
    private SearchUserParameters Filter { get; set; } = new();
    private bool IsSearch;
    private bool resetPaging;
    private bool _loading;

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        table.SetRowsPerPage(new MetaData().PageSize);
        return base.OnAfterRenderAsync(firstRender);
    }

    private async Task<TableData<User>> LoadUsersAsync(TableState state)
    {
        _loading = true;

        PagingResponse<User> response;

        if (resetPaging)
        {
            state.Page = 0;
            state.PageSize = new MetaData().PageSize;
            resetPaging = false;
        }

        if (IsSearch)
        {
            response = await UsersService.SearchAsync(Filter.SetFilters(), state.PageSize, state.Page + 1);
        }
        else
            response = await UsersService.GetAllAsync(state.PageSize, state.Page + 1);

        _loading = false;

        return new TableData<User>() { Items = response.Data, TotalItems = response.MetaData.TotalCount };
    }

    private void OnSearch()
    {
        IsSearch = true;
        resetPaging = true;
        table.ReloadServerData();
    }

    private void OnClearFilters()
    {
        IsSearch = false;
        resetPaging = true;
        Filter = new();
        table.ReloadServerData();
    }

    private void HandleKeyUp(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter")
            OnSearch();
        else if (e.Code == "Escape")
            OnClearFilters();
    }

    private async Task AddEditUser(bool IsEditMode, User user = null)
    {
        var parameters = new DialogParameters();
        DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true };
        parameters.Add("User", user);
        parameters.Add("IsEditMode", IsEditMode);
        var dialogresult = DialogService.Show<AddEditUserDialog>(!IsEditMode ? L["users_new"] : L["users_edit"], parameters, options);
        var result = await dialogresult.Result;

        if (!result.Cancelled)
            await table.ReloadServerData();
    }

    private string? TenantText(User row)
    {
        return !row.MANUFACTURER.HasValue ? "iRobot" : row.Tenant.DisplayName;
    }
}
