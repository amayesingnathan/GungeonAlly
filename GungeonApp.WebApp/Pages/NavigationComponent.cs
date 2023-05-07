using GungeonApp.WebApp.Services;
using Microsoft.AspNetCore.Components;

namespace GungeonApp.WebApp.Pages
{
    public enum PageType
    {
        Null, Home, Ammonomicon, Inventory
    }

    public abstract class NavigationComponent : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public PageManager PageManager { get; set; }

        protected abstract PageType Type { get; }
        protected virtual object? PageContext { get; set; } = null;

        protected void NavigateTo(PageType type)
        {
            PageManager.NavigateTo(NavigationManager, Type, type);
        }

        protected void NavigateBack()
        {
            PageManager.NavigateBack(NavigationManager);
        }
    }
}
