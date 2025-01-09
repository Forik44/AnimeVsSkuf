using AnimeVsSkuf.Scripts.Game.Common;
using AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.MainMenuScreen;
using DI;
using Game.MainMenu;
using R3;

namespace Game
{
    public class MainMenuViewModelsRegistrations
    {
        public static void Register(DIContainer container)
        {
            container.RegisterFactory(c => new MainMenuUIManager(container)).AsSingle();
            container.RegisterFactory(c => new UIMainMenuRootViewModel()).AsSingle();
        }
    }
}