/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Infrastructure.Models.Common;

public class BaseModel
{
    public DateTime LAST_UPDATE_DATE { get; set; } = DateTime.Now;
    public string LAST_UPDATED_BY { get; set; }
    public int? MANUFACTURER { get; set; }
    public string IWIP_VERSION { get; set; } = "IWIP-CLOUD";


    //public int? CreatedBy { get; set; }
    //public DateTime? CreateDate { get; set; }
    //public int? UpdatedBy { get; set; }
    //public DateTime? UpdateDate { get; set; }
}
