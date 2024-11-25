/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Shared.Resources;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace iWip.Client.Models.Auth;

public class SetNewPasswordModel
{
    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [StringLength(85, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.email), ResourceType = typeof(Resource))]
    public string UserName { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [StringLength(35, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.password), ResourceType = typeof(Resource))]
    public string Password { get; set; }

    [Compare("Password", ErrorMessageResourceName = "pass_must_match", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.password), ResourceType = typeof(Resource))]
    public string ConfirmPassword { get; set; }

    public string SessionId { get; set; }

    [JsonIgnore] public InputType PasswordInputType { get; set; } = InputType.Password;
    [JsonIgnore] public InputType ConfirmPasswordInputType { get; set; } = InputType.Password;
}
