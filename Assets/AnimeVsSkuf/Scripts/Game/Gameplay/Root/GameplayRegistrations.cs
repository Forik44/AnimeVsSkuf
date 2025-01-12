using AnimeVsSkuf.Scripts.Game.Common;
using AnimeVsSkuf.Scripts.Game.Gameplay.Commands.Handlers;
using AnimeVsSkuf.Scripts.Game.Gameplay.Services;
using AnimeVsSkuf.Scripts.Game.Settings;
using DI;
using Game.State;
using Game.State.CMD;
using R3;

namespace Game
{
    public static class GameplayRegistrations
    {
        public static void Register(DIContainer container, GameplayEnterParams gameplayEnterParams)
        {
            var gameSettingsProvider = container.Resolve<GameSettingsProvider>();
            var gameSettings = gameSettingsProvider.GameSettings;
            
            var gameStateProvider = container.Resolve<IGameStateProvider>();
            var gameState = gameStateProvider.GameState;
            
            var player = gameplayEnterParams.Player;
            
            container.RegisterInstance(AppConstants.EXIT_SCENE_REQUEST_TAG,new Subject<Unit>());
            
            var cmd = new CommandProcessor(gameStateProvider);
            cmd.RegisterHandler(new CmdResourcesAddHandler(player));
            cmd.RegisterHandler(new CmdResourcesSpendHandler(player));
            container.RegisterInstance<ICommandProcessor>(cmd);

            container.RegisterFactory(_ => new ResourcesService(player.Resources, cmd)).AsSingle();
        }
    }
}