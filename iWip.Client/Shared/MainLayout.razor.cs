/*****************************************************************************

*Copyright(c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Components.Shared;
using iWip.Client.Models.Layout;
using iWip.Client.Services.Layout;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Toolbelt.Blazor.HotKeys2;

namespace iWip.Client.Shared;

public partial class MainLayout : IDisposable
{
    private readonly Palette _darkPalette = new()
    {
        Black = "#27272f",
        Background = "rgb(21,27,34)",
        BackgroundGrey = "#27272f",
        Surface = "#212B36",
        DrawerBackground = "rgb(21,27,34)",
        DrawerText = "rgba(255,255,255, 0.50)",
        DrawerIcon = "rgba(255,255,255, 0.50)",
        AppbarBackground = "#27272f",
        AppbarText = "rgba(255,255,255, 0.70)",
        TextPrimary = "rgba(255,255,255, 0.70)",
        TextSecondary = "rgba(255,255,255, 0.50)",
        ActionDefault = "#adadb1",
        ActionDisabled = "rgba(255,255,255, 0.26)",
        ActionDisabledBackground = "rgba(255,255,255, 0.12)",
        Divider = "rgba(255,255,255, 0.12)",
        DividerLight = "rgba(255,255,255, 0.06)",
        TableLines = "rgba(255,255,255, 0.12)",
        LinesDefault = "rgba(255,255,255, 0.12)",
        LinesInputs = "rgba(255,255,255, 0.3)",
        TextDisabled = "rgba(255,255,255, 0.2)"
    };

    private readonly Palette _lightPalette = new();

    private readonly MudTheme _theme = new()
    {
        Palette = new Palette
        {
            Primary = Colors.Green.Default,
            AppbarBackground = Colors.Red.Default,
            GrayDefault = Colors.Red.Default,
        },
        LayoutProperties = new LayoutProperties
        {
            AppbarHeight = "80px",
            DefaultBorderRadius = "12px"
        },
        Typography = new Typography
        {
            Default = new Default
            {
                FontSize = "0.9rem",
            }
        }
    };

    private bool _canMiniSideMenuDrawer = true;
    private bool _commandPaletteOpen;

    private HotKeysContext? _hotKeysContext;
    private bool _themingDrawerOpen;
    private bool _loadedPreferences;

    private UserPreferences UserPreferences = new()
    {
        Language = "en-US",
        SideMenuDrawerOpen = true,
        ShipmentStatus = false,
        ThemeManager = new()
        {
            IsDarkMode = false,
            PrimaryColor = Colors.Green.Default
        }
    };

    [Inject] private IDialogService _dialogService { get; set; }
    [Inject] private HotKeys _hotKeys { get; set; }
    [Inject] private ILayoutService _layoutService { get; set; }

    public void Dispose()
    {
        _hotKeysContext?.Dispose();
    }

    protected override async Task OnInitializedAsync()
    {
        var result = await _layoutService.GetLayoutPreferences();

        if (!string.IsNullOrEmpty(result.Language))
            UserPreferences = result;

        await UserPreferencesChanged(UserPreferences);

        //_hotKeysContext = _hotKeys.CreateContext()
        //    .Add(ModCode.Meta, Code.K, OpenCommandPalette, new() { Description = "Open command palette." });

        _loadedPreferences = true;
    }

    private async Task UserPreferencesChanged(UserPreferences userPreferences)
    {
        UserPreferences = userPreferences;

        _theme.Palette = UserPreferences.ThemeManager.IsDarkMode ? _darkPalette : _lightPalette;
        _theme.Palette.Primary = UserPreferences.ThemeManager.PrimaryColor;

        await _layoutService.UpdateLayoutPreferences(userPreferences);
        StateHasChanged();
    }

    private async Task OpenCommandPalette()
    {
        if (!_commandPaletteOpen)
        {
            var options = new DialogOptions
            {
                NoHeader = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true
            };

            var commandPalette = _dialogService.Show<CommandPalette>("", options);
            _commandPaletteOpen = true;

            await commandPalette.Result;
            _commandPaletteOpen = false;
        }
    }

    private async Task ToggleSideMenuDrawer()
    {
        UserPreferences = await _layoutService.GetLayoutPreferences();

        UserPreferences.SideMenuDrawerOpen = !UserPreferences.SideMenuDrawerOpen;

        await UserPreferencesChanged(UserPreferences);
    }

    private async Task ThemeManagerChanged(ThemeManager themeManager)
    {
        UserPreferences.ThemeManager = themeManager;

        _theme.Palette = UserPreferences.ThemeManager.IsDarkMode ? _darkPalette : _lightPalette;
        _theme.Palette.Primary = UserPreferences.ThemeManager.PrimaryColor;

        await UserPreferencesChanged(UserPreferences);
    }

}