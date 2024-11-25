/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Client.Models.Invoice;

public class PuchaseOrderModel
{
    public Guid POId { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public PurchaseOrderStatus Status { get; set; }
}