/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using Fluxor.Blazor.Web.Components;
using iWip.Client.Models.SideMenu;
using iWip.Infrastructure.Common.Constants.Permission;
using iWip.Infrastructure.Models.Users;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace iWip.Client.Components.Shared;

public partial class SideMenu : FluxorComponent
{
    [Parameter] public User User { get; set; }

    private List<MenuSectionModel> _menuSections = new()
    {
        new MenuSectionModel
        {
            Title = "application",
            Permissions= new[] { Permissions.Orders.View,  Permissions.Containers.View, Permissions.Shipments.View},
            SectionItems = new List<MenuSectionItemModel>
            {
                new()
                {
                    Title = "home",
                    Icon = Icons.Material.Filled.Home,
                    Href = "/",
                    Permissions = new[] { "*" }
                },
                new()
                {
                    Title = "purchaseorders",
                    Icon = Icons.Material.Filled.ShoppingCart,
                    Href = "/orders",
                    Permissions = new[] { Permissions.Orders.View }
                },
                new()
                {
                    Title = "containers",
                    Icon = Icons.Material.Filled.Blinds,
                    Href = "/containers",
                    Permissions = new[] { Permissions.Containers.View }
                },
                new()
                {
                    Title = "shipment",
                    Icon = Icons.Material.Filled.Train,
                    Href = "/Shipment",
                    Permissions = new[] { Permissions.Shipments.View }
                },
            }
        },

        new MenuSectionModel
        {
            Title = "management",
            Roles = new[] { Roles.Role.HostAdmin, Roles.Role.TenantAdmin },
            SectionItems = new List<MenuSectionItemModel>
            {
                new ()
                {
                    IsParent = true,
                    Title = "security",
                    Icon = Icons.Material.Filled.Person,
                    Roles = new[] { Roles.Role.HostAdmin, Roles.Role.TenantAdmin },
                    MenuItems = new List<MenuSectionSubItemModel>
                    {
                        new ()
                        {
                            Title = "tenants",
                            Href = "/admin/tenants",
                            Roles = new[] { Roles.Role.HostAdmin, Roles.Role.HostUser },
},
                        new ()
                        {
                            Title = "users",
                            Href = "/admin/users",
                            Roles = new[] { Roles.Role.HostAdmin, Roles.Role.TenantAdmin },
                        },
                        new()
                        {
                            Title = "roles_permissions",
                            Href = "/admin/roles",
                            Roles = new[] { Roles.Role.HostAdmin, Roles.Role.TenantAdmin },
                        },
                        //new()
                        //{
                        //    Title = "permissions",
                        //    Href = "/admin/permissions",
                        //    PageStatus = PageStatus.ComingSoon
                        //}
                    }
                },
                //new()
                //{
                //    IsParent = true,
                //    Title = "system",
                //    Icon = Icons.Material.Filled.Devices,
                //    MenuItems = new List<MenuSectionSubItemModel>
                //    {
                //        new()
                //        {
                //            Title = "audittrails",
                //            Href = "/system/audittrails",
                //            PageStatus = PageStatus.ComingSoon
                //        },
                //        new()
                //        {
                //            Title = "log",
                //            Href = "/system/logs",
                //            PageStatus = PageStatus.ComingSoon
                //        }
                //    }
                //}
            }
        }
    };

    private bool SideMenuExpanded { get; set; }
    [EditorRequired][Parameter] public bool SideMenuDrawerOpen { get; set; }
    [EditorRequired][Parameter] public EventCallback<bool> SideMenuDrawerOpenChanged { get; set; }
    [EditorRequired][Parameter] public bool CanMiniSideMenuDrawer { get; set; }
    [EditorRequired][Parameter] public EventCallback<bool> CanMiniSideMenuDrawerChanged { get; set; }

    [Parameter] public EventCallback OnToggleMenuDrawer { get; set; }

    protected override async Task OnInitializedAsync()
    {
        SideMenuExpanded = SideMenuDrawerOpen;
    }

    public async Task ToggleSideMenuDrawerOpen()
    {
        SideMenuExpanded = !SideMenuExpanded;
        await OnToggleMenuDrawer.InvokeAsync();
    }

    private bool IsExpanded { get => SideMenuDrawerOpen || SideMenuExpanded; }
}