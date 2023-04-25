namespace GungeonApp.WebApp.Services
{
    public interface IRefreshService
    {
        event Func<Task> RefreshRequested;
        void RequestRefresh();
    }
}
