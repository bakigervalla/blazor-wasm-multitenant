/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Pages.Admin.Tenants.Components;
using iWip.Client.Services.Common;
using iWip.Infrastructure.Common.Pagination;
using iWip.Infrastructure.Models.Tenants;
using iWip.Infrastructure.Services.Tenants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System;

namespace iWip.Client.Pages.Admin.Tenants;

public partial class Index
{
    private MudTable<Tenant> table;

    [Inject] private ITenantsService TenantsService { get; set; }
    [Inject] private Navigation navigation { get; set; }
    [Inject] private SharedService SharedService { get; set; }

    public IList<Tenant> Tenants { get; set; } = new List<Tenant>();
    private SearchTenantParameters Filter { get; set; } = new();
    private bool IsSearch;
    private bool resetPaging;
    private bool _loading;

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        table.SetRowsPerPage(new MetaData().PageSize);
        return base.OnAfterRenderAsync(firstRender);
    }

    private async Task<TableData<Tenant>> LoadTenantsAsync(TableState state)
    {
        _loading = true;

        PagingResponse<Tenant> response;

        if (resetPaging)
        {
            state.Page = 0;
            state.PageSize = new MetaData().PageSize;
            resetPaging = false;
        }

        if (IsSearch)
        {
            response = await TenantsService.SearchAsync(Filter.SetFilters(), state.PageSize, state.Page + 1);
        }
        else
            response = await TenantsService.GetAllAsync(state.PageSize, state.Page + 1);

        _loading = false;

        return new TableData<Tenant>() { Items = response.Data, TotalItems = response.MetaData.TotalCount };
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

    private async Task AddEditTenant(bool IsEditMode, Tenant tenant = null)
    {
        var parameters = new DialogParameters();
        DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true };
        parameters.Add("Tenant", tenant);
        parameters.Add("IsEditMode", IsEditMode);
        var dialogresult = DialogService.Show<AddEditTenantDialog>(!IsEditMode ? L["tenant_new"] : L["tenants_edit"], parameters, options);
        var result = await dialogresult.Result;

        if (!result.Cancelled)
        {
            await table.ReloadServerData();
            SharedService.RefreshTenantsSwitcher();
        }
    }
}
