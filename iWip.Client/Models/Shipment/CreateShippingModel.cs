/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Shared.Resources;
using iWip.Infrastructure.Models.Shipment;
using System.ComponentModel.DataAnnotations;

namespace iWip.Client.Models.Shipment;

public class CreateShippingModel: BaseModel
{
    // public int CONTAINER_ID { get; set; }
    private DateTime? _EST_TIME_DEPARTURE;

    public int MANUFACTURER { get; set; }

    [StringLength(80, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.shipment_name), ResourceType = typeof(Resource))]
    public string HOUSE_BILL_OF_LADING { get; set; }

    [StringLength(100, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.forwarder), ResourceType = typeof(Resource))]
    public string? FORWARDER { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.shipment_method_1), ResourceType = typeof(Resource))]
    public string? SHIP_VIA { get; set; } // SHIP_METHOD_1

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.shipment_method_2), ResourceType = typeof(Resource))]
    public string? SHIP_METHOD_2 { get; set; }

    [StringLength(100, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.vessel_name), ResourceType = typeof(Resource))]
    public string SHIP_INFO_1 { get; set; } // Vessel Name

    [StringLength(100, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.vehicle_license), ResourceType = typeof(Resource))]
    public string SHIP_INFO_2 { get; set; } // Vehicle License #

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.etd), ResourceType = typeof(Resource))]
    public DateTime? EST_TIME_DEPARTURE { get => _EST_TIME_DEPARTURE; set { SetProperty(ref _EST_TIME_DEPARTURE, value); EST_TIME_ARRIVAL = value.HasValue && value.Value > EST_TIME_ARRIVAL ? null : EST_TIME_ARRIVAL; } }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.eta), ResourceType = typeof(Resource))]
    public DateTime? EST_TIME_ARRIVAL { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.port_of_loading), ResourceType = typeof(Resource))]
    public string? PORT_OF_ORIGIN { get; set; }
    // [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.port_of_discharge), ResourceType = typeof(Resource))]
    public string? PORT_OF_DISCHARGE { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.port_of_destination), ResourceType = typeof(Resource))]
    public string? DESTINATION_PORT { get; set; }

    [StringLength(50, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.house_bol), ResourceType = typeof(Resource))]
    public string? SHIPMENT_NUMBER { get; set; }
    public DateTime? EX_FACTORY_DATE { get; set; }
    public DateTime? DOC_COMPLETE_DATE { get; set; }
    [StringLength(255, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.remarks), ResourceType = typeof(Resource))]
    public string? SHIPMENT_REMARKS { get; set; }

    public bool IS_SHIPPED { get; set; }
    public bool DOC_COMPLETED { get; set; }
    public DateTime LAST_UPDATE_DATE { get; set; } = DateTime.Now;
    public string LAST_UPDATED_BY { get; set; }
    public int HOUSE_BILL_OF_LADING_ID { get; set; }
        
    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.polist), ResourceType = typeof(Resource))]
    public virtual List<ShippedOrders> ShippedOrders { get; set; }
}
