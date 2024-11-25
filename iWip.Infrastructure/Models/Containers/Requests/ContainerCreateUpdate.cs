/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Infrastructure.Models.Containers.Requests;

public class ContainerCreateUpdate
{
    public int MANUFACTURER { get; set; } 
    public int? CONTAINER_ID { get; set; }
    public string CONTAINER_NUMBER { get; set; }
    public DateTime? LAST_UPDATE_DATE { get; set; } = DateTime.Now;
    public string? LAST_UPDATED_BY { get; set; } 
    public string? IWIP_VERSION { get; set; }
    public string? CONTAINER_TYPE { get; set; }
    public string? SEAL_NUMBER { get; set; }
    public DateTime? EX_FACTORY_DATE { get; set; }
    public DateTime? CUSTOMS_CLEARANCE_DATE { get; set; }
    public int? HOUSE_BILL_OF_LADING_ID { get; set; }
    public string? PO_HEADER_ID { get; set; }
}