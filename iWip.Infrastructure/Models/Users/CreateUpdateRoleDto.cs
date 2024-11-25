/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Infrastructure.Models.Users;

public class CreateUpdateRoleDto
{
    public int Id { get; set; }
    public int? Manufacturer { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Status { get; set; } = true;
}
