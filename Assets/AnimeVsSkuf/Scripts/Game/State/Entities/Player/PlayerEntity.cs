using System;
using System.Collections.Generic;
using Game.State.GameResources;
using Game.State.Jobs;

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
        public List<JobData> Jobs;
    }
}