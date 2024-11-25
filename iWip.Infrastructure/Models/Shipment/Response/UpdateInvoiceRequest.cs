/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Infrastructure.Models.Shipment.Response
{
    public class UpdateInvoiceRequest
    {
        public int CONTAINER_ID { get; set; }
        public int CONTAINER_LINE_ID { get; set; }
        public int LINE_LOCATION_ID { get; set; }
        public double? INVOICE_PRICE { get; set; }
        public string COMMERCIAL_INVOICE { get; set; }
        public string? CM_ORDER_NUMBER { get; set; }
        public int? CM_ORDER_LINE { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; } = DateTime.Now;
    }
}
