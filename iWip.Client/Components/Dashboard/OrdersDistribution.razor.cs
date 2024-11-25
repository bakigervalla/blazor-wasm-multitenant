/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Models.Charts;
using iWip.Client.Models.Charts.Chart;
using iWip.Client.Models.Charts.Legend;
using iWip.Client.Models.Charts.XAxis;
using iWip.Client.Models.Charts.YAxis;
using iWip.Infrastructure.Models.Dashboard;
using iWip.Infrastructure.Services.Dashboard;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Utilities;

namespace iWip.Client.Components.Dashboard;

public partial class OrdersDistribution : MudComponentBase
{
    [Inject] private IJSRuntime JsRuntime { get; set; }
    [Inject] private IDashboardService DashboardService { get; set; }

    private IList<int> Years { get; set; } = new List<int>();
    private OrderDistribution OrderDistribution { get; set; } = new();
    private Dictionary<int, List<SeriesModel<int>>> Series { get; set; } = new Dictionary<int, List<SeriesModel<int>>>();
    private int _selectedYear;
    private bool _loading;

    private ChartOptionsModel<SeriesModel<int>, string>? _chartOptions;

    protected override async Task OnInitializedAsync()
    {
        _loading = true;

        await GetOrdersDistribution();
        ChartData();

        _loading = false;
    }

    private async Task GetOrdersDistribution()
    {
        OrderDistribution = await DashboardService.GetOrdersDistributionAsync();
        Series = OrderDistribution.Series;
        Years = Series.Keys.ToList();
        _selectedYear = Series.Keys.First();
    }

    private void ChartData()
    {
        var yAxesMax = Series[_selectedYear].Select(x=> x.Data.Max()).Max() + 100;

        _chartOptions = new ChartOptionsModel<SeriesModel<int>, string>
        {
            Chart = new ChartModel
            {
                Type = ChartTypes.Bar,
                Toolbar = new ChartModel.ToolbarModel
                {
                    Show = false
                },
                Zoom = new ChartModel.ZoomModel
                {
                    Enabled = false
                },
                Width = "100%",
                Height = "450px",
                Id = "orderDistribution"
            },
            Series = Series[_selectedYear],
            XAxis = new XAxisModel<string>
            {
                Categories = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" },
                AxisBorder = new XAxisModel<string>.AxisBorderModel
                {
                    Show = false
                },
                AxisTicks = new XAxisModel<string>.AxisTicksModel
                {
                    Show = false
                },
                Tooltip = new XAxisModel<string>.TooltipModel
                {
                    Enabled = false
                }
            },
            YAxis = new YAxisModel
            {
                AxisTicks = new YAxisModel.AxisTicksModel
                {
                    Show = false
                },
                AxisBorder = new YAxisModel.AxisBorderModel
                {
                    Show = false
                },
                Tooltip = new YAxisModel.TooltipModel
                {
                    Enabled = false
                },
                Max = yAxesMax,
            },
            Legend = new LegendModel
            {
                Position = "top",
                HorizontalAlign = "right"
            },
            Colors = new List<string> { Colors.Blue.Default, Colors.Green.Default, Colors.Orange.Default }, //"var(--mud-palette-primary)", Colors.Yellow.Default
        };
    }

    private async Task UpdateSeries(int yearSelected)
    {
        _selectedYear = yearSelected;
        var dataSeries = Series[_selectedYear];
        _chartOptions.YAxis.Max = dataSeries.Select(x => x.Data.Max()).Max();

        await JsRuntime.InvokeVoidAsync("apex_wrapper.updateOptions", "orderDistribution", _chartOptions);
        await JsRuntime.InvokeVoidAsync("apex_wrapper.updateSeries", "orderDistribution", dataSeries);
    }

    private string Classname => 
        new CssBuilder()
        .AddClass(Class)
        .Build();
}