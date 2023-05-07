using GungeonApp.WebApp.Pages;
using Microsoft.AspNetCore.Components;

namespace GungeonApp.WebApp.Services
{
    public class PageManager
    {
        public int HistoryIndex { get; set; } = 0;
        public PageType[] History { get; set; } = new PageType[5];
        public void NavigateTo(NavigationManager navigationManager, PageType src, PageType dest, object? context = null) 
        {
            History[HistoryIndex] = src;
            HistoryIndex++;
            HistoryIndex %= History.Length;

            switch (dest)
            {
                case PageType.Home:
                    navigationManager.NavigateTo("/");
                    break;

                case PageType.Ammonomicon:
                    navigationManager.NavigateTo("ammonomicon");
                    break;

                case PageType.Inventory:
                    navigationManager.NavigateTo("inventory");
                    break;
            }
        }

        public void NavigateBack(NavigationManager navigationManager)
        {
            HistoryIndex += 4; // Equivalent to -1
            HistoryIndex %= History.Length;
            PageType page = History[HistoryIndex];
            History[HistoryIndex] = PageType.Null;

            switch (page)
            {
                case PageType.Home:
                    navigationManager.NavigateTo("/");
                    break;

                case PageType.Ammonomicon:
                    navigationManager.NavigateTo("ammonomicon");
                    break;

                case PageType.Inventory:
                    navigationManager.NavigateTo("inventory");
                    break;
            }
        }
    }
}
