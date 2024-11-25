/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Infrastructure.Common.Constants;

namespace iWip.Infrastructure.Models.PurchaseOrders.Requests;

public class POContainerContentRequest
{
    public int MANUFACTURER { get; set; }
    public int CONTAINER_LINE_ID { get; set; }
    public DateTime? LAST_UPDATE_DATE { get; set; }
    public string LAST_UPDATED_BY { get; set; }
    public int CONTAINER_ID { get; set; }
    public DateTime? DATE_ADDED { get; set; }
    public string? PO_HEADER_ID { get; set; }
    public int? PO_LINE_ID { get; set; }
    public int? QTY_SHIPPED { get; set; }
}