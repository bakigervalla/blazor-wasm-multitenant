/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using System.Text.Json.Serialization;

namespace iWip.Client.Models.Charts.ForecastDataPoints;

public class ForecastDataPointsModel
{
    [JsonPropertyName("count")] public int Count { get; set; } = 0;
    [JsonPropertyName("fillOpacity")] public double FillOpacity { get; set; } = 0.5;
    [JsonPropertyName("strokeWidth")] public int StrokeWidth { get; set; } = 0;
    [JsonPropertyName("dashArray")] public int DashArray { get; set; } = 4;
}