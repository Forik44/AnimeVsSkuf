using System;
using System.Collections.Generic;
using Game.State.GameResources;

namespace Game.State
{
    [Serializable]
    public class PlayerEntity : Entity
    {
        public string Name;
        public int Level;
        public int Experience;
        public List<int> LevelUpgrades;
        public int Day;
        public List<ResourceData> Resources;
    }
}