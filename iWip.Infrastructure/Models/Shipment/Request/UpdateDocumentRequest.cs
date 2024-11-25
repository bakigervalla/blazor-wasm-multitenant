/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Infrastructure.Models.Shipment.Request;
public class UpdateDocumentRequest
{
    public int ID { get; set; }
    public string? STATUS_COMMENT { get; set; }
    public DateTime LAST_UPDATE_DATE { get; set; } = DateTime.Now;
    public string LAST_UPDATED_BY { get; set; }
}