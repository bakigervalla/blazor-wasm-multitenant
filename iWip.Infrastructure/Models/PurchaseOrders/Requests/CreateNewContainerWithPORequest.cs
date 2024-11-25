/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Infrastructure.Models.PurchaseOrders.Requests;

public class CreateNewContainerWithPORequest
{
    public int MANUFACTURER { get; set; } 
    public string CONTAINER_NUMBER { get; set; }
    public DateTime? LAST_UPDATE_DATE { get; set; }
    public string? LAST_UPDATED_BY { get; set; } 
    public string? CONTAINER_TYPE { get; set; }
    public string? SEAL_NUMBER { get; set; }
    public int? HOUSE_BILL_OF_LADING_ID { get; set; }
    public string? PO_HEADER_ID { get; set; }
    public int PO_LINE_ID { get; set; }
    public int? QTY_SHIPPED { get; set; }
}

