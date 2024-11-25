/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Client.Models.Auth;

public class ResetPasswordFormModel
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? DisplayName { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
}