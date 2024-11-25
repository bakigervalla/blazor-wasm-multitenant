/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Infrastructure.Models.Shipment.Request
{
    public class DocumentCompleteRequest
    {
        public string HOUSE_BILL_OF_LADING { get; set; }
        public bool DOC_COMPLETED { get; set; }
        public DateTime DOC_COMPLETE_DATE { get; set; } = DateTime.Now;
        public DateTime LAST_UPDATE_DATE { get; set; } = DateTime.Now;
        public string LAST_UPDATED_BY { get; set; }

    }
}
