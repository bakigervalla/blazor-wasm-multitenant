/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace iWip.Client.Models.Auth;

public class LoginFormModel
{
    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [StringLength(85, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.email), ResourceType = typeof(Resource))]
    public string? UserName { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [StringLength(35, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.password), ResourceType = typeof(Resource))]
    public string? Password { get; set; }
}
