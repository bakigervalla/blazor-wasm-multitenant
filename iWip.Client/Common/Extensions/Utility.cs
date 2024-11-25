/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Models.Shipment;
using Microsoft.AspNetCore.Components;

namespace iWip.Client.Common.Extensions;

public static class Utility
{
    public static string ToShortDateStringOrEmpty(this DateTime? dateTime)
    {
        return dateTime.HasValue ? dateTime.Value.ToShortDateString() : null;
    }
    public static bool IsEnumEquals(this string enumString, UploadFileType value)
    {
        if (Enum.TryParse<UploadFileType>(enumString, out var v))
        {
            return value == v;
        }

        return false;
    }

    public static bool IsEnumEquals(this string enumString, TagType value)
    {
        if (Enum.TryParse<TagType>(enumString, out var v))
        {
            return value == v;
        }

        return false;
    }

    public static byte[] StreamToByteArray(this Stream input)
    {
        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }

    public static string InitialUpperCase(this string s)
    {
        if (String.IsNullOrEmpty(s))
            return s;
        if (s.Length == 1)
            return s.ToUpper();
        return s.Remove(1).ToUpper() + s.Substring(1);
    }

    public static void ReloadPage(this NavigationManager manager)
    {
        manager.NavigateTo(manager.Uri, true);
    }

    public static string ConvertToBase64(this Stream stream)
    {
        if (stream is MemoryStream memoryStream)
        {
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        var bytes = new Byte[(int)stream.Length];

        stream.Seek(0, SeekOrigin.Begin);
        stream.Read(bytes, 0, (int)stream.Length);

        return Convert.ToBase64String(bytes);
    }

    public static string ReplaceApiURL(this string baseUrl, string keyword, string replacement)
    {
        Uri uri = new Uri(baseUrl);
        string host = uri.Host;

        int index = host.IndexOf(keyword);
        if (index >= 0)
        {
            string newHost = host.Substring(0, index) + replacement + host.Substring(index + keyword.Length);
            return baseUrl.Replace(host, newHost);
        }

        return baseUrl;
    }
}
