/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using Fluxor;
using iWip;
using iWip.Client;
using iWip.Client.Common.Extensions;
using iWip.Client.Common.Helpers;
using iWip.Client.Services;
using iWip.Client.Services.Auth;
using iWip.Client.Services.Common;
using iWip.Client.Services.Layout;
using iWip.Infrastructure.Models.Users;
using iWip.Infrastructure.Services.Auth;
using iWip.Infrastructure.Services.Containers;
using iWip.Infrastructure.Services.Dashboard;
using iWip.Infrastructure.Services.Http;
using iWip.Infrastructure.Services.PurchaseOrders;
using iWip.Infrastructure.Services.Shipment;
using iWip.Infrastructure.Services.Tenants;
using iWip.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor.Services;
using System.Globalization;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLocalization();

// Existing HttpClient registration
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var baseURL = builder.HostEnvironment.BaseAddress;
var uri = new Uri(baseURL);
// var apiBaseUrl = $"{uri.Scheme}://api.{uri.Host}/api/v1/";
var apiBaseUrl = "https://localhost:44340/api/v1/";

builder.Services.AddHttpClient<HttpService>("iwipcloud", async client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services
    .AddMudServices()
    .AddHotKeys2()
    .AddLocalization()
    .AddAuthorizationCore();

builder.Services
        .AddSingleton<Navigation>()
        .AddTransient<INotificationsService, NotificationsService>()
        .AddScoped<ILayoutService, LayoutService>()
        .AddScoped<ILocalStorageService, LocalStorageService>()
        .AddScoped<IHttpService, HttpService>()
        .AddSingleton<SharedService>()
        .AddScoped<ITenantsService, TenantService>()
        .AddScoped<IDashboardService, DashboardService>()
        .AddScoped<IPurchaseOrderService, PurchaseOrderService>()
        .AddScoped<IContainersService, ContainersService>()
        .AddScoped<IShipmentService, ShipmentService>()
        .AddScoped<IUsersService, UsersService>()
        .AddScoped<IAuthService, AuthService>()
        .AddScoped<AuthenticationStateProvider, AuthStateProvider>()
        .AddScoped<User>()
        .AddScoped<IAuthorizationPolicyProvider, PermissionPolicyProvider>() // Authorization Policies
        .AddAutoMapperConfiguration(builder.Configuration);

builder.Services.AddFluxor(o =>
{
    o.ScanAssemblies(typeof(Program).Assembly);
    o.UseReduxDevTools(rdt =>
    {
        rdt.Name = "iwipcloud";
    });
});


var host = builder.Build();

//Setting culture of the application
var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
var language = await jsInterop.InvokeAsync<string>("getLanguage");

if (!string.IsNullOrEmpty(language))
{
    var culture = new CultureInfo(language);
    CultureInfo.DefaultThreadCurrentCulture = culture;
    CultureInfo.DefaultThreadCurrentUICulture = culture;
}
else
{
    var culture = new CultureInfo("en-US");
    CultureInfo.DefaultThreadCurrentCulture = culture;
    CultureInfo.DefaultThreadCurrentUICulture = culture;
}

await host.RunAsync();