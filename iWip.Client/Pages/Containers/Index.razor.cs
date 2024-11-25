/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Infrastructure.Common.Pagination;
using iWip.Infrastructure.Models.Containers;
using iWip.Infrastructure.Routes;
using iWip.Infrastructure.Services.Containers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using Container = iWip.Infrastructure.Models.Containers.Container;

namespace iWip.Client.Pages.Containers;

public partial class Index
{
    private MudTable<Container> table;

    [Inject] private IContainersService ContainersService { get; set; }
    [Inject] private Navigation navigation { get; set; }

    public IList<Container> Containers { get; set; } = new List<Container>();
    private SearchContainerParameters Filter { get; set; } = new();
    private bool IsSearch;
    private bool resetPaging;
    private bool _loading;

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        table.SetRowsPerPage(new MetaData().PageSize);
        return base.OnAfterRenderAsync(firstRender);
    }

    private async Task<TableData<Container>> LoadContainersAsync(TableState state)
    {
        _loading = true;

        PagingResponse<Container> response;

        if (resetPaging)
        {
            state.Page = 0;
            state.PageSize = new MetaData().PageSize;
            resetPaging = false;
        }

        if (IsSearch)
        {
            response = await ContainersService.SearchContainersAsync(Filter.SetFilters(), state.PageSize, state.Page + 1);
        }
        else
            response = await ContainersService.GetAllAsync(state.PageSize, state.Page + 1);

        _loading = false;

        return new TableData<Container>() { Items = response.Data, TotalItems = response.MetaData.TotalCount };
    }

    public void ShowDetails(Container item)
    {
        item.ShowDetails = !item.ShowDetails;
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

    public void ContainerDetails(int? Id)
    {
        navigation.NavigateTo(Paths.ContainerDetails(Id.Value));
    }

    private void HandleKeyUp(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter")
            OnSearch();
        else if (e.Code == "Escape")
            OnClearFilters();
    }
}