/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Models.Charts;
using iWip.Infrastructure.Models.Dashboard;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;

namespace iWip.Client.Components.Dashboard;

public partial class DataCard : MudComponentBase
{
    private ChartOptionsModel<SeriesModel<int>, int>? _chartOptions = null;

    private string Classname =>
        new CssBuilder()
            .AddClass(Class)
            .Build();

    [EditorRequired] [Parameter] public string Title { get; set; } = string.Empty;
    [EditorRequired] [Parameter] public string ChartId { get; set; } = string.Empty;
    [Parameter] public string BarColor { get; set; } = "var(--mud-palette-primary)";
    [EditorRequired][Parameter] public Summary Summary { get; set; } = new();

}