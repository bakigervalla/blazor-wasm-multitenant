/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Infrastructure.Models.Containers;

namespace iWip.Infrastructure.Models.Shipment.Response;
public class ShippingListResponse
{
    public int MANUFACTURER { get;set; }
    public string HOUSE_BILL_OF_LADING { get; set; }
    public int HOUSE_BILL_OF_LADING_ID { get; set; }
    public string? FORWARDER { get; set; }
    public string? SHIP_VIA { get; set; } // SHIP_METHOD_1
    public string? SHIP_METHOD_2 { get; set; }
    public string SHIP_INFO_1 { get; set; } // Vessel Name
    public DateTime? EST_TIME_DEPARTURE { get; set; }
    public DateTime? EST_TIME_ARRIVAL { get; set; }
    public string? PORT_OF_ORIGIN { get; set; }
    public string? DESTINATION_PORT { get; set; }
    public string? PORT_OF_DISCHARGE { get; set; }
    public string? PURCHASE_ORDER_NUMBER { get; set; }
    public int? LINE_NUM { get; set; }
    public string? ITEM_NUMBER { get; set; }
    public bool? IS_SHIPPED { get; set; }
    public DateTime? DOC_COMPLETE_DATE { get; set; }
    public bool? DOC_COMPLETED { get; set; }

    public int? CONTAINER_ID { get => Container?.CONTAINER_ID; }
    public string CONTAINER_NUMBER { get => Container?.CONTAINER_NUMBER; }

    public Container? Container => Containers.FirstOrDefault();

    public IEnumerable<Container> Containers { get; set; }
}