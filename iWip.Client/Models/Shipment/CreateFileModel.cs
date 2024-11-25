/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Common.Extensions;
using iWip.Client.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace iWip.Client.Models.Shipment
{
    public class CreateFileModel
    {
        public CreateFileModel()
        {
            LAST_UPDATE_DATE = DateTime.Now;
            STATUS = "Active";
    }

        public string SHIPMENT_NAME { get; set; }
        public int HOUSE_BILL_OF_LADING_ID { get; set; }
        public string FILE_TYPE { get; set; }
        public string FILE_TAG { get; set; }
        public string FILE_FORMAT { get; set; }
        public string FILE_MIMETYPE { get; set; }
        public string FILE_NAME { get; set; }

        [RequiredIf(nameof(FILE_TYPE), UploadFileType.OTHER, ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "{0} length can't be more than {1} characters.")]
        [MinLength(6, ErrorMessage = "{0} requires {1} characters length.")]
        [Display(Name = nameof(Resource.description), ResourceType = typeof(Resource))]
        public string? FILE_DESCRIPTION { get; set; }
        public string? FILE_COMMENT { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public string LAST_UPDATED_BY { get; set; }
        public string STATUS { get; set; }

        public TagType Tag { get; set; }        
        public UploadFileType FileType { get; set; }
        public string File { get; set; }
    }

    public enum TagType
    {
        Standard,
        Draft,
        Final
    }
}
