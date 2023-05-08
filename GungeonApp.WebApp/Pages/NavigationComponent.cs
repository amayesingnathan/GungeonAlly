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
        public PageManager PageManager { get; set; }

        protected abstract PageType Type { get; }

        protected void NavigateTo(PageType type)
        {
            PageManager.NavigateTo(Type, type);
        }

        protected void NavigateBack()
        {
            PageManager.NavigateBack();
        }
    }
}
