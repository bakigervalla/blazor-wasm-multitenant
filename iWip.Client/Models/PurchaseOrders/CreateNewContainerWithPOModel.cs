/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Shared.Resources;
using iWip.Infrastructure.Models.Shipment;
using System.ComponentModel.DataAnnotations;

namespace iWip.Client.Models.PurchaseOrders
{
    public class CreateNewContainerWithPOModel
    {
        [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(80, ErrorMessage = "{0} length can't be more than {1} characters.")]
        [Display(Name = nameof(Resource.container_number), ResourceType = typeof(Resource))]
        public string CONTAINER_NUMBER { get; set; }

        [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(80, ErrorMessage = "{0} length can't be more than {1} characters.")]
        [Display(Name = nameof(Resource.seal_number), ResourceType = typeof(Resource))]
        public string SEAL_NUMBER { get; set; }

        [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(50, ErrorMessage = "{0} length can't be more than {1} characters.")]
        [Display(Name = nameof(Resource.container_type), ResourceType = typeof(Resource))]
        public string CONTAINER_TYPE { get; set; }

        [Required]
        public int? QTY_SHIPPED { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; } = DateTime.Now;

        public int? HOUSE_BILL_OF_LADING_ID { get => Shipping?.HOUSE_BILL_OF_LADING_ID; }

        [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = nameof(Resource.shipment), ResourceType = typeof(Resource))]
        public ShippingHeader? Shipping { get; set; }

    }
}
