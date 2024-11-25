/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Infrastructure.Models.Users;

public class CreateUpdateUserDto
{
    public int Id { get; set; }
    public int? Manufacturer { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsHost { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsActive { get; set; }
    public string Sub { get; set; }
    public IEnumerable<UserRole>? UserRoles { get; set; }
}
