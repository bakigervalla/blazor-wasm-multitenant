/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Shared.Resources;
using iWip.Infrastructure.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace iWip.Client.Models.Users;

public class CreateUpdateUser
{
    public int Id { get; set; }
    public int? Manufacturer { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceName = "invalid_email", ErrorMessageResourceType = typeof(Resource))]
    [StringLength(85, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.email), ResourceType = typeof(Resource))]
    public string Email { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [StringLength(30, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.first_name), ResourceType = typeof(Resource))]
    public string FirstName { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [StringLength(85, ErrorMessage = "{0} length can't be more than {1} characters.")]
    [Display(Name = nameof(Resource.last_name), ResourceType = typeof(Resource))]
    public string LastName { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.users_type), ResourceType = typeof(Resource))]
    public bool IsHost { get; set; }

    public bool IsAdmin { get; set; }

    public bool IsActive { get; set; }

    [Required(ErrorMessageResourceName = "cannot_be_empty", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.role), ResourceType = typeof(Resource))]
    public IEnumerable<UserRole>? UserRoles { get; set; }

    [JsonIgnore]
    public IEnumerable<Role>? Roles { get; set; }
}
