using System;
using System.Collections.Generic;
using Game.State.GameResources;

namespace Game.State
{
    [Serializable]
    public class GameState
    {
        public int PlayerEntityId;
        public List<PlayerEntity> Players;
        
        public int CreatePlayerId()
        {
            return PlayerEntityId++;
        }
    }
}