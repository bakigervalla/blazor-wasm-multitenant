namespace iWip.Client.Services.Common
{
    public class SharedService
    {
        public event Action OnTenantCreateRefreshTenantSwitcher;

        public void RefreshTenantsSwitcher()
        {
            OnTenantCreateRefreshTenantSwitcher?.Invoke();
        }
    }
}
