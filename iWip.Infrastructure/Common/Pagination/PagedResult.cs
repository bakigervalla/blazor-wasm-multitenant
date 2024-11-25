/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using System.Text.Json.Serialization;

namespace iWip.Infrastructure.Common.Pagination
{
    public class PagedResult<T>
    {
        [JsonPropertyName("page")]
        public int? Page { get; set; } = null;
        [JsonPropertyName("results")]
        public List<T> Results { get; set; } = new List<T>();
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
    }
}
