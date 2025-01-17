using System.Collections.Generic;
using AnimeVsSkuf.Scripts.Game.Settings;
using Game.State;
using Game.State.CMD;
using Game.State.GameResources;
using GoogleSpreadsheets;

namespace Game.Gameplay
{
    public class CmdCreatePlayerHandler : ICommandHandler<CmdCreatePlayer>
    {
        private readonly GameStateProxy _gameState;
        private readonly GameSettings _gameSettings;

        public CmdCreatePlayerHandler(GameStateProxy gameState, GameSettings gameSettings)
        {
            _gameState = gameState;
            _gameSettings = gameSettings;
        }
        public bool Handle(CmdCreatePlayer command)
        {
            var playerId = _gameState.CreatePlayerId();
            var newPlayerEntity = new PlayerEntity
            {
                Id = playerId,
                Name = command.Name,
                Level = command.Level,
                Day = command.Day,
                Resources = new List<ResourceData>
                {
                    new() {ResourceType = ResourceType.Money, Amount = int.Parse(_gameSettings.GetConstantValue(ConstantsType.DefaultResourceMoney))},
                    new() {ResourceType = ResourceType.Weariness, Amount = int.Parse(_gameSettings.GetConstantValue(ConstantsType.DefaultResourceWeariness))},
                    new() {ResourceType = ResourceType.Weight, Amount = int.Parse(_gameSettings.GetConstantValue(ConstantsType.DefaultResourceWeight))},
                }
            };
            
            var newPlayerEntityProxy = new PlayerEntityProxy(newPlayerEntity);
            _gameState.Players.Add(newPlayerEntityProxy);

            return true;
        }
    }
}