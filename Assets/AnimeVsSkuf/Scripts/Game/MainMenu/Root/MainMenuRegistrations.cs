using AnimeVsSkuf.Scripts.Game.Common;
using AnimeVsSkuf.Scripts.Game.Settings;
using DI;
using Game.Gameplay;
using Game.MainMenu;
using Game.State;
using Game.State.CMD;
using R3;

namespace Game
{
    public class MainMenuRegistrations
    {
        public static void Register(DIContainer container, MainMenuEnterParams mainMenuEnterParams)
        {
            var gameSettingsProvider = container.Resolve<GameSettingsProvider>();
            var gameSettings = gameSettingsProvider.GameSettings;
            
            var gameStateProvider = container.Resolve<IGameStateProvider>();
            var gameState = gameStateProvider.GameState;
            
            container.RegisterInstance(AppConstants.EXIT_SCENE_REQUEST_TAG,new Subject<PlayerEntityProxy>());

            var cmd = new CommandProcessor(gameStateProvider);
            cmd.RegisterHandler(new CmdCreatePlayerHandler(gameState, gameSettings));
            cmd.RegisterHandler(new CmdDeletePlayerHandler(gameState));
            container.RegisterInstance<ICommandProcessor>(cmd);
            
            container.RegisterFactory(_ => new PlayersService(gameState.Players, gameSettings, cmd, container.Resolve<Subject<PlayerEntityProxy>>(AppConstants.EXIT_SCENE_REQUEST_TAG))).AsSingle();
        }
    }
}