/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Common.Extensions;
using iWip.Client.Models.Layout;
using iWip.Client.Models.Notification;
using iWip.Client.Services;
using iWip.Client.Services.Auth;
using iWip.Infrastructure.Models.Tenants;
using iWip.Infrastructure.Models.Users;
using iWip.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

namespace iWip.Client.Components.Shared;

public partial class NavMenu
{
    private IEnumerable<NotificationModel> _activeNotifications;

    [Inject] private INotificationsService NotificationsService { get; set; }
    [Inject] AuthenticationStateProvider authStateProvider { get; set; }
    [Inject] IAuthService authService { get; set; }

    [EditorRequired][Parameter] public ThemeManager ThemeManager { get; set; }
    [EditorRequired][Parameter] public bool CanMiniSideMenuDrawer { get; set; }
    [EditorRequired][Parameter] public EventCallback ToggleSideMenuDrawer { get; set; }
    [EditorRequired][Parameter] public EventCallback OpenCommandPalette { get; set; }
    [EditorRequired][Parameter] public User User { get; set; }
    private int? TenantId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _activeNotifications = await NotificationsService.GetActiveNotifications();
        TenantId = User.MANUFACTURER;
    }

    private async Task SetTenant(Tenant e)
    {
        User.MANUFACTURER = e?.ID;
        User.Tenant = e;
        HoverClass = "";
        ShowDropDown = false;

        authService.User.MANUFACTURER = User.MANUFACTURER;
        await ((AuthStateProvider)authStateProvider).UpdateAuthenticationState(authService.User);
    }

    private void RefreshPage(bool e)
    {
        if (!e && TenantId != User.MANUFACTURER) {
            TenantId = User.MANUFACTURER;
            NavigationManager.ReloadPage();
        }
    }

    string HoverClass { get; set; }
    bool ShowDropDown { get; set; }
    void MouseOver(MouseEventArgs e) { HoverClass = "hover-tenant"; }
    void MouseOut(MouseEventArgs e) { HoverClass = ""; ShowDropDown = false; }
    void MouceClick(MouseEventArgs e) { ShowDropDown = true; }
}