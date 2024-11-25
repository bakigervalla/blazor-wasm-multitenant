/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Client.Common.Extensions;
public static class Enumerable
{
    public static IEnumerable<T> Add<T>(this IEnumerable<T> e, T value)
    {
        foreach (var cur in e)
        {
            yield return cur;
        }
        yield return value;
    }
}
