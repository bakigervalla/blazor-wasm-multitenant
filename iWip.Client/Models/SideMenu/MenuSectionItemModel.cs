/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Client.Models.SideMenu;

public class MenuSectionItemModel
{
    public string Title { get; set; }
    public string Icon { get; set; }
    public string Href { get; set; }
    public PageStatus PageStatus { get; set; } = PageStatus.Completed;
    public string[] Permissions { get; set; }
    public string[] Roles { get; set; }
    public bool IsParent { get; set; }
    public List<MenuSectionSubItemModel> MenuItems { get; set; }
}