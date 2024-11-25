/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Client.Models.SideMenu;

public class MenuSectionModel
{
    public string Title { get; set; }
    public List<MenuSectionItemModel> SectionItems { get; set; }
    public string[] Permissions { get; set; }
    public string[] Roles { get; set; }
}