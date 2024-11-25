/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Infrastructure.Models.Users;
using System.Text.Json.Serialization;

namespace iWip.Infrastructure.Models.Tenants;

public class Tenant
{
    public Tenant()
    {
        if (Logo != null)
            LogoImage = string.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String(Logo));
    }

    public int ID { get; set; }
    public string Email { get; set; }
    public string ShortName { get; set; }
    public string Name { get; set; }
    public string ContactPerson { get; set; }
    public byte[]? Logo { get; set; }
    public string? LogoImage { get; }
    public bool IsActive { get; set; }

    [JsonIgnore]
    public string DisplayName { get => ID == 0 ? "" : $"{ID} - {ShortName}"; }

    public DateTime LAST_UPDATE_DATE { get; set; }
    public string LAST_UPDATED_BY { get; set; }

    // public User? LAST_UPDATED_BY_USER { get => Users?.Find(x => x.Id.Equals(LAST_UPDATED_BY)); }

    public virtual List<User> Users { get; set; }
    public List<Role> Roles { get; set; }
}
