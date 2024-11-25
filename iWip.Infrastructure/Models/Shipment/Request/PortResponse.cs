/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Infrastructure.Models.Shipment.Request
{
    public class PortResponse
    {
        public IList<PortDetails> Forwarders { get; set; }
        public IList<PortDetails> Loading { get; set; }
        public IList<PortDetails> Discharge { get; set; }
        public IList<PortDetails> Destination { get; set; }
    }
}
