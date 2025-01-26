using System;
using System.Collections.Generic;
using Game.State.GameResources;
using Game.State.Unlock;
using UnityEngine.Serialization;

namespace Game.State.Jobs
{
    [Serializable]
    public class JobData
    {
        public int Id;
        public string Name;
        public List<ResourceType> CostResourceTypes;
        public List<double> CostResourceValues;
        public List<ResourceType> RewardResourceTypes;
        public List<double> RewardResourceValues;
        public List<IUnlockData> Unlocks;
        public int Experience;
    }
}