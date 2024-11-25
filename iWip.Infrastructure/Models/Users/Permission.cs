/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using System.Text.Json.Serialization;

namespace iWip.Infrastructure.Models.Users;

public class Permission
{
    public int ID { get; set; }
    public int RoleID { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    public DateTime UpdatedDate { get; set; }
    public Role Role { get; set; }

    [JsonIgnore] public bool IsUpdated { get; set;}
}
