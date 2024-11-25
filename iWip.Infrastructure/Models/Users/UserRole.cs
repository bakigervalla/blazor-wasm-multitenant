/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Infrastructure.Models.Users;

public class UserRole
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public int RoleID { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;

    public User User { get; set; }
    public Role Role { get; set; }
}
