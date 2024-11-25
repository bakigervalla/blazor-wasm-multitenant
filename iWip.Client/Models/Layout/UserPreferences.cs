/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Client.Models.Layout;

public class UserPreferences
{
    public ThemeManager ThemeManager { get; set; }
    public bool SideMenuDrawerOpen { get; set; }
    public string Language { get; set; }
    public bool ShipmentStatus { get; set; }
}

public class ThemeManager
{
    public bool IsDarkMode { get; set; }
    public string PrimaryColor { get; set; }

}