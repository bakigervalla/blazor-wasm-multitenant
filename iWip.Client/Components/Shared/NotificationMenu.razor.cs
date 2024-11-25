/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Models.Notification;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using MudBlazor.Utilities;

namespace iWip.Client.Components.Shared;

public partial class NotificationMenu : MudComponentBase
{
    private string Classname =>
        new CssBuilder()
            .AddClass(Class)
            .Build();
    
    [Parameter] public IEnumerable<NotificationModel>? Notifications { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnClickViewAll { get; set; }
}