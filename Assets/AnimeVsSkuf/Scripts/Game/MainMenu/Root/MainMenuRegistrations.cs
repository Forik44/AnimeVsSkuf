using DI;
using Game.Gameplay;
using Game.MainMenu;
using Game.State;
using Game.State.CMD;

namespace Game
{
    public class MainMenuRegistrations
    {
        public static void Register(DIContainer container, MainMenuEnterParams mainMenuEnterParams)
        {
            var gameStateProvider = container.Resolve<IGameStateProvider>();
            var gameState = gameStateProvider.GameState;

            var cmd = new CommandProcessor(gameStateProvider);
            cmd.RegisterHandler(new CmdCreatePlayerHandler(gameState));
            container.RegisterInstance<ICommandProcessor>(cmd);
            
            container.RegisterFactory(_ => new PlayersService(gameState.Players, cmd)).AsSingle();
        }
    }
}