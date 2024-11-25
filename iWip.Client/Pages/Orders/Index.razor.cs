/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Infrastructure.Common.Pagination;
using iWip.Infrastructure.Models.Orders;
using iWip.Infrastructure.Models.Shipment;
using iWip.Infrastructure.Services.PurchaseOrders;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace iWip.Client.Pages.Orders
{
    public partial class Index
    {
        private MudTable<POLine> table;

        [Inject] private IPurchaseOrderService POService { get; set; }

        public MetaData MetaData { get; set; } = new();
        private SearchShippedOrdersParameters Filter { get; set; } = new();
        private bool IsSearch;
        private bool resetPaging;
        private bool _loading;

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            table.SetRowsPerPage(new MetaData().PageSize);
            return base.OnAfterRenderAsync(firstRender);
        }

        private async Task<TableData<POLine>> LoadPOLinesAsync(TableState state)
        {
            _loading = true;

            PagingResponse<POLine> response;

            if (resetPaging)
            {
                state.Page = 0;
                state.PageSize = new MetaData().PageSize;
                resetPaging = false;
            }

            if (IsSearch)
            {
                response = await POService.SearchPOLinesAsync(Filter.SetFilters(), 0, state.PageSize, state.Page + 1);
            }
            else
                response = await POService.GetPOLinesAsync(state.PageSize, state.Page + 1);

            _loading = false;

            return new TableData<POLine>() { Items = response.Data, TotalItems = response.MetaData.TotalCount };
        }

        public void ShowDetails(POHeader item)
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

        private void HandleKeyUp(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
                OnSearch();
            else if (e.Code == "Escape")
                OnClearFilters();
        }
    }
}
