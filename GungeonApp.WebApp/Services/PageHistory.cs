using GungeonApp.WebApp.Pages;

namespace GungeonApp.WebApp.Services
{
    public class PageHistory
    {
        public int Index { get; set; } = 0;
        public PageType[] Pages { get; set; } = new PageType[5];
    }
}
