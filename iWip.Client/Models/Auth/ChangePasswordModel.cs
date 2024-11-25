/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Shared.Resources;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace iWip.Client.Models.Auth;

public class ChangePasswordModel
{
    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [StringLength(35, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.current_password), ResourceType = typeof(Resource))]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [StringLength(35, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.password), ResourceType = typeof(Resource))]
    public string Password { get; set; }

    [Compare("Password", ErrorMessageResourceName = "pass_must_match", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.confirm_password), ResourceType = typeof(Resource))]
    public string ConfirmPassword { get; set; }

    public string SessionId { get; set; }

    [JsonIgnore] public InputType CurrentPasswordInputType { get; set; } = InputType.Password;
    [JsonIgnore] public InputType PasswordInputType { get; set; } = InputType.Password;
    [JsonIgnore] public InputType ConfirmPasswordInputType { get; set; } = InputType.Password;
    [JsonIgnore] public string CurrentPasswordIcon { get; set; } = Icons.Material.Filled.VisibilityOff;
    [JsonIgnore] public string PasswordIcon { get; set; } = Icons.Material.Filled.VisibilityOff;
    [JsonIgnore] public string ConfirmPasswordIcon { get; set; } = Icons.Material.Filled.VisibilityOff;
}
