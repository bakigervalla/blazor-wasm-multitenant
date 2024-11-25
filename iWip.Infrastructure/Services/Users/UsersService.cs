/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Infrastructure.Common;
using iWip.Infrastructure.Common.Pagination;
using iWip.Infrastructure.Models.Users;
using iWip.Infrastructure.Services.Http;
using System.Text.Json;

namespace iWip.Infrastructure.Services.Users;

public interface IUsersService
{
    Task<PagingResponse<User>> GetAllAsync(int pageSize, int page = 1, CancellationToken cancellationToken = default);
    Task<User> GetByIdAsync(int id);
    Task<PagingResponse<User>> SearchAsync(string filter, int pageSize, int page);

    Task<HttpResponse<string>> AddNewAsync(CreateUpdateUserDto user);
    Task<HttpResponse<string>> UpdateAsync(CreateUpdateUserDto user);
    Task<HttpResponse<string>> DeleteAsync(int Id);

    Task<IEnumerable<Role>> GetRolesAsync();
    Task<HttpResponse<string>> AddNewRoleAsync(CreateUpdateRoleDto role);
    Task<HttpResponse<string>> UpdateRoleAsync(CreateUpdateRoleDto role);
    Task<HttpResponse<string>> DeleteRoleAsync(int Id);
    Task<IEnumerable<Permission>> GetPermissionsByRoleAsync(int roleId);
    Task<HttpResponse<string>> UpdatePermissionsAsync(IList<Permission> permissions);
}

public class UsersService : IUsersService
{
    private readonly IHttpService _httpService;
    private readonly JsonSerializerOptions _options;

    public UsersService(IHttpService httpService)
    {
        _httpService = httpService;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<PagingResponse<User>> GetAllAsync(int pageSize, int page = 1, CancellationToken cancellationToken = default)
    {
        var response = await _httpService.GetAsync(Routes.Endpoints.UsersGetAll(pageSize, page));

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var pagingResponse = new PagingResponse<User>
        {
            Data = JsonSerializer.Deserialize<List<User>>(content, _options),
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
        };

        return pagingResponse;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var response = await _httpService.GetAsync(Routes.Endpoints.UserGetById(id));

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<User>(content, _options);
    }

    public async Task<PagingResponse<User>> SearchAsync(string filter, int pageSize, int page)
    {
        var response = await _httpService.GetAsync(Routes.Endpoints.UsersSearch(filter, pageSize, page));

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        var pagingResponse = new PagingResponse<User>
        {
            Data = JsonSerializer.Deserialize<List<User>>(content, _options),
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
        };

        return pagingResponse;
    }

    public async Task<HttpResponse<string>> AddNewAsync(CreateUpdateUserDto user)
    {
        var response = await _httpService.PostAsync(Routes.Endpoints.Users, user);

        return new HttpResponse<string> { HttpStatusCode = response.StatusCode };
    }

    public async Task<HttpResponse<string>> UpdateAsync(CreateUpdateUserDto user)
    {
        var response = await _httpService.PutAsync(Routes.Endpoints.Users, user);

        return new HttpResponse<string> { HttpStatusCode = response.StatusCode };
    }

    public async Task<HttpResponse<string>> DeleteAsync(int Id)
    {
        var response = await _httpService.DeleteAsync(Routes.Endpoints.UserDelete(Id));

        return new HttpResponse<string> { HttpStatusCode = response.StatusCode };
    }

    // Permissions
    public async Task<IEnumerable<Role>> GetRolesAsync()
    {
        var response = await _httpService.GetAsync(Routes.Endpoints.RolesGetAll);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException(content);

        return JsonSerializer.Deserialize<IEnumerable<Role>>(content, _options);
    }

    public async Task<HttpResponse<string>> AddNewRoleAsync(CreateUpdateRoleDto role)
    {
        var response = await _httpService.PostAsync(Routes.Endpoints.RoleAddEdit, role);

        return new HttpResponse<string> { HttpStatusCode = response.StatusCode };
    }

    public async Task<HttpResponse<string>> UpdateRoleAsync(CreateUpdateRoleDto role)
    {
        var response = await _httpService.PutAsync(Routes.Endpoints.RoleAddEdit, role);

        return new HttpResponse<string> { HttpStatusCode = response.StatusCode };
    }

    public async Task<HttpResponse<string>> DeleteRoleAsync(int Id)
    {
        var response = await _httpService.DeleteAsync(Routes.Endpoints.RoleDelete(Id));

        return new HttpResponse<string> { HttpStatusCode = response.StatusCode };
    }

    public async Task<IEnumerable<Permission>> GetPermissionsByRoleAsync(int roleId)
    {
        var response = await _httpService.GetAsync(Routes.Endpoints.PermissionsGetAllByRoleId(roleId));

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException(content);

        return JsonSerializer.Deserialize<IEnumerable<Permission>>(content, _options);
    }

    public async Task<HttpResponse<string>> UpdatePermissionsAsync(IList<Permission> permissions)
    {
        var response = await _httpService.PutAsync(Routes.Endpoints.PermissionsUpdate, permissions);

        return new HttpResponse<string> { HttpStatusCode = response.StatusCode };
    }
}
