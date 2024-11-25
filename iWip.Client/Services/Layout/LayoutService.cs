/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Models.Layout;
using iWip.Infrastructure.Services.Http;

namespace iWip.Client.Services.Layout;

public interface ILayoutService
{
    Task<UserPreferences> GetLayoutPreferences();
    Task<UserPreferences> UpdateLayoutPreferences(UserPreferences userPreferences);
}

public class LayoutService : ILayoutService
{
    private ILocalStorageService _localStorage { get; set; }
    private const string Key = "iWipUserPreferences";

    public LayoutService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<UserPreferences> GetLayoutPreferences()
    {
        try
        {
            var userPreferences = await _localStorage.GetItem<UserPreferences?>(Key);

            if (userPreferences is not null)
                return userPreferences;
            else
                return new UserPreferences();
        }
        catch (Exception ex)
        {
            await _localStorage.RemoveItem(Key);
            return new UserPreferences();
        }
    }

    public async Task<UserPreferences> UpdateLayoutPreferences(UserPreferences userPreferences)
    {
        await _localStorage.SetItem<UserPreferences>(Key, userPreferences);
        return userPreferences;
    }

}
