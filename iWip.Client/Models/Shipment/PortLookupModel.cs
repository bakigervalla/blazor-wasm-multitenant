/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Client.Models.Shipment
{
    public class PortLookupModel
    {
        public string[] Loading { get; set; }
        public string[] Discharge { get; set; }
        public string[] Destination { get; set; }
    }
}
