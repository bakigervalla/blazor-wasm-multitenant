/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Client.Common.Extensions
{
    public static class TruncateText
    {
        public static string Truncate(this string value, int maxchars = 200)
        {
            return value.Length <= maxchars ? value : value.Substring(0, maxchars) + "...";
        }
    }
}
