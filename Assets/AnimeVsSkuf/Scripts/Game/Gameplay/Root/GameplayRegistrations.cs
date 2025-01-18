using System.Linq;
using AnimeVsSkuf.Scripts.Game.Common;
using AnimeVsSkuf.Scripts.Game.Gameplay.Commands.Handlers;
using AnimeVsSkuf.Scripts.Game.Gameplay.Services;
using AnimeVsSkuf.Scripts.Game.Settings;
using DI;
using Game.Gameplay;
using Game.MainMenu;
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
            
            var playerID = gameplayEnterParams.PlayerId;
            var player = gameState.Players.FirstOrDefault(e => e.Id == playerID);
            
            container.RegisterInstance(player);
            container.RegisterInstance(AppConstants.EXIT_SCENE_REQUEST_TAG,new Subject<int>());
            
            var cmd = new CommandProcessor(gameStateProvider);
            cmd.RegisterHandler(new CmdResourcesAddHandler(player));
            cmd.RegisterHandler(new CmdResourcesSpendHandler(player));
            cmd.RegisterHandler(new CmdResourcesSetHandler(player));
            container.RegisterInstance<ICommandProcessor>(cmd);
            
            container.RegisterFactory(_ => new ResourcesService(player.Resources, cmd)).AsSingle();
            cmd.RegisterHandler(new CmdStartNewDayHandler(player, container.Resolve<ResourcesService>()));
            
            container.RegisterFactory(_ => new PlayersService(gameState.Players, gameSettings, cmd, container.Resolve<Subject<int>>(AppConstants.EXIT_SCENE_REQUEST_TAG))).AsSingle();
        }
    }
}