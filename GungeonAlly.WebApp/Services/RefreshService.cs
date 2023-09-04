namespace GungeonAlly.WebApp.Services
{
    public class RefreshService : IRefreshService
    {
        public event Func<Task>? RefreshRequested;
        public async Task RequestRefresh()
        {
            await RefreshRequested?.Invoke();
        }
    }
}
