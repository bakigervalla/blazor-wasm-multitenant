/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using System.ComponentModel;
using System.Reflection;

namespace iWip.Infrastructure.Common.Constants.Permission;

public static class Permissions
{
    public static List<string> GetRegisteredPermissions()
    {
        var permissions = new List<string>();
        foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c =>
                     c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
        {
            var propertyValue = prop.GetValue(null);
            if (propertyValue is not null)
                permissions.Add((string)propertyValue);
        }

        return permissions;
    }

    public static List<string> GeneratePermissionsForModule(string module)
    {
        return new List<string>() {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
    }

    //[DisplayName("AuditTrails")]
    //[Description("AuditTrails Permissions")]
    //public static class AuditTrails
    //{
    //    public const string View = "Permissions.AuditTrails.View";
    //    public const string Search = "Permissions.AuditTrails.Search";
    //    public const string Export = "Permissions.AuditTrails.Export";
    //}

    //[DisplayName("Logs")]
    //[Description("Logs Permissions")]
    //public static class Logs
    //{
    //    public const string View = "Permissions.Logs.View";
    //    public const string Search = "Permissions.Logs.Search";
    //    public const string Export = "Permissions.Logs.Export";
    //    public const string Purge = "Permissions.Logs.Purge";
    //}

    //[DisplayName("Tenants")]
    //[Description("Tenants Permissions")]
    //public static class Tenants
    //{
    //    public const string View = "Permissions.Tenants.View";
    //    public const string Create = "Permissions.Tenants.Create";
    //    public const string Edit = "Permissions.Tenants.Edit";
    //    public const string Delete = "Permissions.Tenants.Delete";
    //}

    //[DisplayName("Users")]
    //[Description("Users Permissions")]
    //public static class Users
    //{
    //    public const string View = "Permissions.Users.View";
    //    public const string Create = "Permissions.Users.Create";
    //    public const string Edit = "Permissions.Users.Edit";
    //    public const string Delete = "Permissions.Users.Delete";
    //}

    //[DisplayName("Roles")]
    //[Description("Roles Permissions")]
    //public static class Roles
    //{
    //    public const string View = "Permissions.Roles.View";
    //    public const string Create = "Permissions.Roles.Create";
    //    public const string Edit = "Permissions.Roles.Edit";
    //    public const string Delete = "Permissions.Roles.Delete";
    //}

    [DisplayName("Containers")]
    [Description("Containers Permissions")]
    public static class Containers
    {
        public const string View = "Permissions.Containers.View";
        public const string Create = "Permissions.Containers.Create";
        public const string Edit = "Permissions.Containers.Edit";
        public const string Delete = "Permissions.Containers.Delete";
    }

    [DisplayName("ContainerContent")]
    [Description("ContainerContent Permissions")]
    public static class ContainerContent
    {
        public const string View = "Permissions.ContainerContent.View";
        public const string Create = "Permissions.ContainerContent.Create";
        public const string Edit = "Permissions.ContainerContent.Edit";
        public const string Delete = "Permissions.ContainerContent.Delete";
    }

    [DisplayName("Orders")]
    [Description("Orders Permissions")]
    public static class Orders
    {
        public const string View = "Permissions.Orders.View";
        public const string Create = "Permissions.Orders.Create";
        public const string Edit = "Permissions.Orders.Edit";
        public const string Delete = "Permissions.Orders.Delete";
    }

    [DisplayName("Shipments")]
    [Description("Shipments Permissions")]
    public static class Shipments
    {
        public const string View = "Permissions.Shipment.View";
        public const string Create = "Permissions.Shipment.Create";
        public const string Edit = "Permissions.Shipment.Edit";
        public const string Delete = "Permissions.Shipment.Delete";
    }

    [DisplayName("Documents")]
    [Description("Documents Permissions")]
    public static class Documents
    {
        public const string View = "Permissions.Documents.View";
        public const string Upload = "Permissions.Documents.Upload";
        public const string Invalidate = "Permissions.Documents.Invalidate";
    }
}

public static class Roles
{
    public static class Role
    {
        public const string HostAdmin = "HostAdmin";
        public const string HostUser = "HostUser";
        public const string TenantAdmin = "TenantAdmin";
        public const string TenantUser = "TenantUser";
    }
}
