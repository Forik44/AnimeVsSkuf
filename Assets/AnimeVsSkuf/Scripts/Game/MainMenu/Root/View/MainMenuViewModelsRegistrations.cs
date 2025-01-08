using DI;
using Game.MainMenu;

namespace Game
{
    public class MainMenuViewModelsRegistrations
    {
        public static void Register(DIContainer container)
        {
            container.RegisterFactory(c => new UIMainMenuRootViewModel(c.Resolve<PlayersService>())).AsSingle();
        }
    }
}