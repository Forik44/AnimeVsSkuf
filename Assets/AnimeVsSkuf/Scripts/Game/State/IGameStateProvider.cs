using AnimeVsSkuf.Scripts.Game.Settings;
using R3;

namespace Game.State
{
    public interface IGameStateProvider
    {
        public GameStateProxy GameState { get; }

        public Observable<GameStateProxy> LoadGameState(GameSettingsProvider gameSettingsProvider);
        public Observable<bool> SaveGameState();
        public Observable<bool> ResetGameState();
    }
}