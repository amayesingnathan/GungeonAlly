using GungeonAlly.WebApp.Pages;
using Microsoft.AspNetCore.Components;

namespace GungeonAlly.WebApp.Services
{
    public class PageManager
    {
        public PageManager(NavigationManager navigationManager, PageHistory history)
        {
            _NavigationManager = navigationManager;
            History = history;
        }

        private readonly NavigationManager _NavigationManager;
        private PageHistory History;
        public void NavigateTo(PageType src, PageType dest, object? context = null) 
        {
            History.Pages[History.Index] = src;
            History.Index++;
            History.Index %= History.Pages.Length;

            switch (dest)
            {
                case PageType.Home:
                    _NavigationManager.NavigateTo("/");
                    break;

                case PageType.Ammonomicon:
                    _NavigationManager.NavigateTo("ammonomicon");
                    break;

                case PageType.Inventory:
                    _NavigationManager.NavigateTo("inventory");
                    break;
            }
        }

        public void NavigateBack()
        {
            History.Index += 4; // Equivalent to -1
            History.Index %= History.Pages.Length;
            PageType page = History.Pages[History.Index];
            History.Pages[History.Index] = PageType.Null;

            switch (page)
            {
                case PageType.Null:
                case PageType.Home:
                    _NavigationManager.NavigateTo("/");
                    break;

                case PageType.Ammonomicon:
                    _NavigationManager.NavigateTo("ammonomicon");
                    break;

                case PageType.Inventory:
                    _NavigationManager.NavigateTo("inventory");
                    break;
            }
        }
    }
}
