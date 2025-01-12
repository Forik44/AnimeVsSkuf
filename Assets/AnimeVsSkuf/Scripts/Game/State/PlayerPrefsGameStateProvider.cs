using System.Collections.Generic;
using System.Linq;
using AnimeVsSkuf.Scripts.Game.Settings;
using Game.State.GameResources;
using GoogleSpreadsheets;
using R3;
using UnityEngine;

namespace Game.State
{
    public class PlayerPrefsGameStateProvider : IGameStateProvider
    {
        private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);
        
        public GameStateProxy GameState { get; private set; }
        
        private GameSettings _gameSettings;
        private GameState _gameStateOrigin;
        
        public Observable<GameStateProxy> LoadGameState(GameSettingsProvider gameSettingsProvider)
        {
            _gameSettings = gameSettingsProvider.GameSettings;
            
            if (!PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                GameState = CreateGameStateFromSettings();
                Debug.Log("Game State created from settings: " + JsonUtility.ToJson(_gameStateOrigin, true));

                SaveGameState();
            }
            else
            {
                var json = PlayerPrefs.GetString(GAME_STATE_KEY);
                _gameStateOrigin = JsonUtility.FromJson<GameState>(json);
                GameState = new GameStateProxy(_gameStateOrigin);
                
                Debug.Log("Game State loaded: " + json);
            }

            return Observable.Return(GameState);
        }

        public Observable<bool> SaveGameState()
        {
            var json = JsonUtility.ToJson(_gameStateOrigin, true);
            PlayerPrefs.SetString(GAME_STATE_KEY, json);
            
            return Observable.Return(true);
        }

        public Observable<bool> ResetGameState()
        {
            GameState = CreateGameStateFromSettings();
            SaveGameState();
            
            return Observable.Return(true);
        }
        
        private GameStateProxy CreateGameStateFromSettings()
        {
            //TODO: Подтягивать из настроек
            _gameStateOrigin = new GameState
            {
                Players = new List<PlayerEntity>()
            };
            
            return new GameStateProxy(_gameStateOrigin);
        }
    }
}