/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Infrastructure.Models.Dashboard;
using iWip.Infrastructure.Services.Dashboard;
using Microsoft.AspNetCore.Components;

namespace iWip.Client.Pages;

public partial class Index
{

    [Inject] private IDashboardService DashboardService { get; set; }
    
    private Quantities Quantities { get; set; }
    private string chartOne = "C-1";
    private string chartTwo = "C-2";
    
    protected override async Task OnInitializedAsync()
    {
        await GetInsights();
    }

    private async Task GetInsights()
    {
        var result = await DashboardService.GetQuantitiesAsync();
        Quantities = result;
    }

}