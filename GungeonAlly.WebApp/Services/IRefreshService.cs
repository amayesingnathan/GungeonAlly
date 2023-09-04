namespace GungeonAlly.WebApp.Services
{
    public interface IRefreshService
    {
        event Func<Task>? RefreshRequested;
        Task RequestRefresh();
    }
}
