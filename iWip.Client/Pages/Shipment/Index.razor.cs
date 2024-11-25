/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Models.Layout;
using iWip.Client.Services.Layout;
using iWip.Infrastructure.Common.Pagination;
using iWip.Infrastructure.Models.Shipment;
using iWip.Infrastructure.Models.Shipment.Response;
using iWip.Infrastructure.Services.Shipment;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace iWip.Client.Pages.Shipment;

public partial class Index
{
    private MudTable<ShippingListResponse> table;

    [EditorRequired][Parameter] public EventCallback<bool> SideMenuDrawerOpenChanged { get; set; }

    [Inject] private IShipmentService ShipmentService { get; set; }
    [Inject] private ILayoutService _layoutService { get; set; }

    private Func<TableState, Task<TableData<ShippingListResponse>>> Items;
    public MetaData MetaData { get; set; } = new();
    private SearchShippingParameters Filter { get; set; } = new();
    private UserPreferences UserPreferences { get; set; }
    private string SwitchLabel => Filter.Status ? L["shipment_switch_completed"] : L["shipment_switch_open"];
    private bool _shipmentStatus;
    private bool ShipmentStatus { get => _shipmentStatus; set { _shipmentStatus = value; Filter.Status = value; Task.Run(() => StatuchCheckedChanged()); } }
    private bool resetPaging;
    private bool _loading;

    protected override async Task OnInitializedAsync()
    {
        UserPreferences = await _layoutService.GetLayoutPreferences();
        ShipmentStatus = UserPreferences.ShipmentStatus;
        Items = new Func<TableState, Task<TableData<ShippingListResponse>>>(LoadShippingsAsync);
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        table.SetRowsPerPage(new MetaData().PageSize);
        return base.OnAfterRenderAsync(firstRender);
    }

    private async Task<TableData<ShippingListResponse>> LoadShippingsAsync(TableState state)
    {
        _loading = true;

        PagingResponse<ShippingListResponse> response;

        if (resetPaging)
        {
            state.Page = 0;
            state.PageSize = new MetaData().PageSize;
            resetPaging = false;
        }

        response = await ShipmentService.SearchShippingsAsync(Filter.SetFilters(), state.PageSize, state.Page + 1);

        _loading = false;

        return new TableData<ShippingListResponse>() { Items = response.Data, TotalItems = response.MetaData.TotalCount };
    }

    //public void ShowDetails(ShippingHeader item)
    //{
    //    item.ShowDetails = !item.ShowDetails;
    //}

    private void OnSearch()
    {
        resetPaging = true;
        table.ReloadServerData();
    }

    private void OnClearFilters()
    {
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

    private async Task StatuchCheckedChanged()
    {
        UserPreferences.ShipmentStatus = Filter.Status;
        await _layoutService.UpdateLayoutPreferences(UserPreferences);

        OnSearch();
    }

    private string HightlightSelected(ShippingListResponse shipment, int index)
    {
        if (shipment.DOC_COMPLETED.HasValue && shipment.DOC_COMPLETED.Value)
            return "!bg-lime-50";
        return "";
    }
}