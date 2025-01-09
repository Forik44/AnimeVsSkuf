using System;
using System.Collections.Generic;
using AnimeVsSkuf.Scripts.Game.Settings;
using Game.Gameplay;
using Game.State;
using Game.State.CMD;
using GoogleSpreadsheets;
using ObservableCollections;
using R3;

namespace Game.MainMenu
{
    public class PlayersService
    {
        private readonly GameSettings _gameSettings;
        private readonly ICommandProcessor _cmd;
        private readonly ObservableList<PlayerViewModel> _allPlayers = new();
        private readonly Dictionary<int, PlayerViewModel> _playersMap = new();

        public IObservableCollection<PlayerViewModel> AllPlayers => _allPlayers;

        public PlayersService(IObservableCollection<PlayerEntityProxy> players, GameSettings gameSettings, ICommandProcessor cmd)
        {
            _gameSettings = gameSettings;
            _cmd = cmd;

            foreach (var playerEntity in players)
            {
                CreatePlayerViewModel(playerEntity);
            }

            players.ObserveAdd().Subscribe(e =>
            {
                CreatePlayerViewModel(e.Value);
            });

            players.ObserveRemove().Subscribe(e =>
            {
                RemovePlayerViewModel(e.Value);
            });
        }
        
        public bool CreatePlayer(string name)
        {
            var command = new CmdCreatePlayer(name);
            var result = _cmd.Process(command);

            return result;
        }

        public bool DeletePlayer(int playerEntityId)
        {
            throw new NotImplementedException();
        }

        private void CreatePlayerViewModel(PlayerEntityProxy playerEntity)
        {
            var defaultName = _gameSettings.Constants.Find(e => e.Id == ConstantsConverter.GetConstantByType(ConstantsType.DefaultPlayerName));
            var playerViewModel = new PlayerViewModel(playerEntity, defaultName.Value ,this);
            
            _allPlayers.Add(playerViewModel);
            _playersMap[playerEntity.Id] = playerViewModel;
        }

        private void RemovePlayerViewModel(PlayerEntityProxy playerEntity)
        {
            if (_playersMap.TryGetValue(playerEntity.Id, out var playerViewModel))
            {
                _allPlayers.Remove(playerViewModel);
                _playersMap.Remove(playerEntity.Id);
            }
        }
    }
}