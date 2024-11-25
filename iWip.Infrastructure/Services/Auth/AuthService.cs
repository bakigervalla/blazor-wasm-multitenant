/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Infrastructure.Common;
using iWip.Infrastructure.Models.Auth;
using iWip.Infrastructure.Models.Users;
using iWip.Infrastructure.Services.Http;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace iWip.Infrastructure.Services.Auth;

public interface IAuthService
{
    User User { get; }

    Task<HttpResponse<User>> LoginAsync(LoginRequest loginRequest);
    Task<HttpResponse<User>> LoginWithOKTAAsync(CreateUpdateUserDto user);

    Task<HttpResponse<User>> SetNewPasswordAsync(SetNewPasswordRequest newPasswordModel);
    Task<HttpResponse<string>> LogOutAsync();

    Task<HttpResponse<string>> ForgotPasswordAsync(string email);
    Task<HttpResponse<string>> ResetPasswordAsync(UserConfirmForgotPasswordRequest resetPassword);
    Task<HttpResponse<string>> ChangePasswordAsync(ChangePasswordRequest changePassword);
    void SetUserFromLocalStorage(User user);
}

public class AuthService : IAuthService
{
    public User User { get; private set; }

    private readonly IHttpService _httpService;
    private ILocalStorageService _localStorageService;

    public async Task Initialize()
    {
        User = await _localStorageService.GetItem<User>("iwipcloud_user");
    }

    public AuthService(IHttpService httpService, ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
        _httpService = httpService;
    }
    public async Task<HttpResponse<User>> LoginAsync(LoginRequest loginRequest)
    {
        var response = await _httpService.PostAsync(Routes.Endpoints.SignIn, loginRequest);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
            return new HttpResponse<User> { HttpStatusCode = HttpStatusCode.Unauthorized };

        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.Locked)
            {
                var newPasswordResponse = await response.Content.ReadFromJsonAsync<SetNewPasswordResponse>();

                return new HttpResponse<User> { HttpStatusCode = response.StatusCode, Response = new User { SessionId = newPasswordResponse.Message } };
            }
            else
                return new HttpResponse<User> { HttpStatusCode = response.StatusCode };
        }

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IgnoreReadOnlyProperties = true,
        };

        User = await response.Content.ReadFromJsonAsync<User>(options);

        return new HttpResponse<User>
        {
            Response = User,
            HttpStatusCode = response.StatusCode
        };
    }

    public async Task<HttpResponse<User>> LoginWithOKTAAsync(CreateUpdateUserDto user)
    {
        var response = await _httpService.PostAsync(Routes.Endpoints.SignInWithOKTA, user);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
            return new HttpResponse<User> { HttpStatusCode = HttpStatusCode.Unauthorized };

        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.Locked)
            {
                var newPasswordResponse = await response.Content.ReadFromJsonAsync<SetNewPasswordResponse>();

                return new HttpResponse<User> { HttpStatusCode = response.StatusCode, Response = new User { SessionId = newPasswordResponse.Message } };
            }
            else
                return new HttpResponse<User> { HttpStatusCode = response.StatusCode };
        }

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IgnoreReadOnlyProperties = true,
        };

        User = await response.Content.ReadFromJsonAsync<User>(options);

        return new HttpResponse<User>
        {
            Response = User,
            HttpStatusCode = response.StatusCode
        };
    }

    public async Task<HttpResponse<User>> SetNewPasswordAsync(SetNewPasswordRequest newPasswordModel)
    {
        var response = await _httpService.PostAsync(Routes.Endpoints.SetNewPassword, newPasswordModel);

        if (!response.IsSuccessStatusCode)
        {
            var res = await response.Content.ReadFromJsonAsync<SetNewPasswordResponse>();
            return new HttpResponse<User> { HttpStatusCode = response.StatusCode, Message = res.Message };
        }

        User = await response.Content.ReadFromJsonAsync<User>();

        await _localStorageService.SetItem("iwipcloud_user", User);

        return new HttpResponse<User>
        {
            Response = User,
            HttpStatusCode = response.StatusCode
        };
    }

    public async Task<HttpResponse<string>> LogOutAsync()
    {
        await _localStorageService.RemoveItem("iwipcloud_user");
        await _localStorageService.RemoveItem("iwipcloud_token");

        User = null;

        return new HttpResponse<string>
        {
            HttpStatusCode = System.Net.HttpStatusCode.OK
        };

    }

    public async Task<HttpResponse<string>> ForgotPasswordAsync(string email)
    {
        var response = await _httpService.GetAsync(Routes.Endpoints.ForgotPassword(email));

        return new HttpResponse<string> { HttpStatusCode = response.StatusCode };
    }

    public async Task<HttpResponse<string>> ResetPasswordAsync(UserConfirmForgotPasswordRequest resetPassword)
    {
        var response = await _httpService.PostAsync(Routes.Endpoints.ResetPassword, resetPassword);

        return new HttpResponse<string> { HttpStatusCode = response.StatusCode };
    }

    public async Task<HttpResponse<string>> ChangePasswordAsync(ChangePasswordRequest changePassword)
    {
        var response = await _httpService.PostAsync(Routes.Endpoints.ChangePassword, changePassword);

        return new HttpResponse<string> { HttpStatusCode = response.StatusCode };
    }

    public void SetUserFromLocalStorage(User user)
    {
        User = user;
    }
}