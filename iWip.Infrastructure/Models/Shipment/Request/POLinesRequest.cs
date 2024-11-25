/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Infrastructure.Models.Common;

namespace iWip.Infrastructure.Models.Shipment.Request
{
    public class POLinesRequest : BaseModel
    {
        public int HOUSE_BILL_OF_LADING_PO_ID { get; set; }
        public int HOUSE_BILL_OF_LADING_ID { get; set; }
        public string PO_HEADER_ID { get; set; }
        public int PO_LINE_ID { get; set; }
        public int LINE_LOCATION_ID { get; set; }
    }
}
