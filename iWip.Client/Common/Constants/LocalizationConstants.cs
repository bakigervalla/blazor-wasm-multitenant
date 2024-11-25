/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWipCommon.Constants;

public static class LocalizationConstants
{
    public const string ResourcesPath = "Resources";
    public static readonly LanguageCode[] SupportedLanguages = {
            new LanguageCode
            {
                Code = "en-US",
                DisplayName= "English"
            },
            new LanguageCode
            {
                Code = "ms-MY",
                DisplayName = "Malaysia"
            },
            
            new LanguageCode
            {
                Code = "zh-CN",
                DisplayName = "中文"
            }
        };
}

public class LanguageCode
{
    public string DisplayName { get; set; } = "en-US";
    public string Code { get; set; } = "English";
}
