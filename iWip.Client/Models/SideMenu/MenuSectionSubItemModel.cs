/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Client.Models.SideMenu;

public class MenuSectionSubItemModel
{
    public string Title { get; set; }
    public string Href { get; set; }
    public string[]? Roles { get; set; }
    public string[] Permissions { get; set; }
    public PageStatus PageStatus { get; set; } = PageStatus.Completed;
    public string? Target { get; set; }
}