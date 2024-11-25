/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Shared.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace iWip.Client.Models.Tenants;

public class CreateUpdateTenant
{
    public int? ID { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceName = "invalid_email", ErrorMessageResourceType = typeof(Resource))]
    [StringLength(85, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.irobot_email), ResourceType = typeof(Resource))]
    public string Email { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [StringLength(50, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.short_name), ResourceType = typeof(Resource))]
    public string ShortName { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [StringLength(255, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.long_name), ResourceType = typeof(Resource))]
    public string Name { get; set; }

    [StringLength(85, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.irobot_contact), ResourceType = typeof(Resource))]
    public string ContactPerson { get; set; }

    public byte[]? Logo { get; set; }
    [JsonIgnore]
    public string? LogoImage { get; set; }

    public bool IsActive { get; set; } = true;
}
