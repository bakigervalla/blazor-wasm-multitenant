/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Infrastructure.Routes;

public static class Endpoints
{

    // Dashboard
    public static string Dashboard => "dashboard";
    public static string GetDashboardQuantities => $"{Dashboard}/insights/quantities";
    public static string GetDashboardOrdersDueThisWeek => $"{Dashboard}/insights/duebyweek";
    public static string GetDashboardOrdersDistribution => $"{Dashboard}/insights/distribution";


    // PO
    public static string PO => "po";
    public static string GetOrders(int pageSize, int page) => $"{PO}?pagesize={pageSize}&pagenumber={page}";
    public static string SearchOrders(string filter, int pageSize, int page) => $"{PO}/search?filter={filter}&pagesize={pageSize}&pagenumber={page}";
    public static string GethOrderByLineId(int lineId) => $"{PO}/{lineId}";
    public static string GetPOContainers(string poHeaderId, int pageSize, int page) => $"{PO}/containers?poheaderid={poHeaderId}&pagesize={pageSize}&pagenumber={page}";
    public static string SearchPOContainers(string filter, string poHeaderId, int pageSize, int page) => $"{PO}/containers/search?filter={filter}&poheaderid={poHeaderId}&pagesize={pageSize}&pagenumber={page}";
    public static string AddPOToContainer => $"{PO}/contents";


    // POLines
    public static string POLine => "po/lines";
    public static string GetOrderById(int Id) => $"{POLine}/{Id}";


    // Containers
    public static string Container => "containers";
    public static string DeleteContainerById(int Id) => $"{Container}/{Id}";
    public static string GetContainers(int pageSize, int page) => $"{Container}?pagesize={pageSize}&pagenumber={page}";
    public static string SearchContainers(string filter, int pageSize, int page) => $"{Container}/search?filter={filter}&pagesize={pageSize}&pagenumber={page}";
    public static string GetContainerById(int Id) => $"{Container}/{Id}";
    public static string GetContainersByHeaderId(string headerId) => $"{Container}/headerId/{headerId}";
    public static string GetContainerTypesAll => $"{Container}/containertypes";
    public static string NewContainerAndAddPO => $"{Container}/po";

    // Container Contents
    public static string ContainerContent => "containers/contents";
    public static string GetContainersContentAll(int pageSize, int page) => $"{ContainerContent}?pagesize={pageSize}&pagenumber={page}";
    public static string SearchContainersContent(string filter, int pageSize, int page) => $"{ContainerContent}/search?filter={filter}&pagesize={pageSize}&pagenumber={page}";
    public static string GetContainerContentsById(int id, int page) => $"{ContainerContent}/{id}?pagenumber={page}";
    public static string DeleteContainerContent => $"{ContainerContent}/delete";


    // Shipping
    public static string Shipping => "shipping";
    public static string GetShippingAllLookup(string search) => $"{Shipping}/lookup?search={search}";
    public static string GetForwardersLookup => $"{Shipping}/forwarders";
    public static string GetPortsLookup(string lovMode, string lovType, string lovRegion) => $"{Shipping}/ports?lovMode={lovMode}&lovType={lovType}&lovRegion={lovRegion}";
    public static string GetShippingMethodsLookup => $"{Shipping}/methods";
    public static string GetShippingById(string houseBillOfLading) => $"{Shipping}/{houseBillOfLading}";
    public static string AddShippedPOs => $"{Shipping}/housebillofladings";
    //public static string RemoveShippedPOById => $"{Shipping}/po/delete";
    public static string GetAllShippings(int pageSize, int page) => $"{Shipping}?pagesize={pageSize}&pagenumber={page}";
    public static string SearchShippings(string filter, int pageSize, int page) => $"{Shipping}/search?filter={filter}&pagesize={pageSize}&pagenumber={page}";
    public static string SearchShippedOrders(string filter, int houseBillOfLading, int pageSize, int page) => @$"{Shipping}/po/shipped/search?filter={filter}&housebillofladingid={houseBillOfLading}&pagesize={pageSize}&pagenumber={page}";
    public static string ShipmentSearchOrders(string filter, int pageSize, int page) => $"{Shipping}/po/search?filter={filter}&pagesize={pageSize}&pagenumber={page}";
    public static string SearchInvoices(string filter, int pageSize, int page) => $"{Shipping}/invoices?filter={filter}&pagesize={pageSize}&pagenumber={page}";
    public static string UpdateInvoices => $"{Shipping}/invoices/update";
    public static string DocComplete => $"{Shipping}/doccomplete";
    public static string UnlockShipment => $"{Shipping}/unlock";
    public static string GetShippingDocumentsByHouseBillOfLadingIdAsync(int houseBillOfLadingId) => $"{Shipping}/documents/{houseBillOfLadingId}";
    public static string ShippingDocuments => $"{Shipping}/documents";

    // Auth
    public static string Auth => "auth";
    public static string SignIn => $"{Auth}/signin";
    public static string SignInWithOKTA => $"{Auth}/signinwithokta";
    public static string SetNewPassword => $"{Auth}/setnewpassword";
    public static string SignUp => $"{Auth}/signup";
    public static string ForgotPassword(string email) => $"{Auth}/forgotpassword?username={email}";
    public static string ResetPassword => $"{Auth}/resetpassword";
    public static string ChangePassword => $"{Auth}/changepassword";

    // Tenants
    public static string Tenants => "tenants";
    public static string TenantsGetAll( int pageSize, int page) => $"{Tenants}?pagesize={pageSize}&pagenumber={page}";
    public static string TenantGetById(int id) => $"{Tenants}/{id}";
    public static string TenantsSearch(string filter, int pageSize, int page) => $"{Tenants}/search?filter={filter}&pagesize={pageSize}&pagenumber={page}";
    public static string GetTenantsAllLookup(string search) => $"{Tenants}/lookup?search={search}";
    public static string TenantDelete(int Id) => $"{Tenants}/{Id}";

    // Users
    public static string Users => "users";
    public static string UsersGetAll(int pageSize, int page) => $"{Users}?pagesize={pageSize}&pagenumber={page}";
    public static string UserGetById(int id) => $"{Users}/{id}";
    public static string UsersSearch(string filter, int pageSize, int page) => $"{Users}/search?filter={filter}&pagesize={pageSize}&pagenumber={page}";
    public static string UserDelete(int Id) => $"{Users}/{Id}";

    public static string RolesGetAll => $"{Users}/roles";
    public static string RoleAddEdit => $"{Users}/roles";
    public static string RoleDelete(int roleId) => $"{Users}/roles/{roleId}";
    public static string PermissionsGetAllByRoleId(int roleId) => $"{Users}/permissions/{roleId}";
    public static string PermissionsUpdate => $"{Users}/permissions";
}