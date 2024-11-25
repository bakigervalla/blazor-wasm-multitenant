/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace iWip.Client.Models.Users;

public class CreateUpdateRole
{
    public int Id { get; set; }
    public int? Manufacturer { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [StringLength(85, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.name), ResourceType = typeof(Resource))]
    public string Name { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [StringLength(30, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.description), ResourceType = typeof(Resource))]
    public string Description { get; set; }

    public bool Status { get; set; } = true;
}
