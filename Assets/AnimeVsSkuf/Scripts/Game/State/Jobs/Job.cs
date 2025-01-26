using Game.State.GameResources;
using Game.State.Unlock;
using ObservableCollections;
using R3;

namespace Game.State.Jobs
{
    public class Job
    {
        public int Id { get; }
        public ReactiveProperty<string> Name;
        public ObservableList<ResourceType> CostResourceTypes { get; } = new();
        public ObservableList<double> CostResourceValues { get; } = new();
        public ObservableList<ResourceType> RewardResourceTypes { get; } = new();
        public ObservableList<double> RewardResourceValues { get; } = new();
        public ObservableList<IUnlockData> Unlocks { get; } = new();
        public ReactiveProperty<int> Experience;
        
        public readonly JobData Origin;

        public Job(JobData data)
        {
            Origin = data;
            
            Id = data.Id;
            
            Name = new ReactiveProperty<string>(data.Name);
            Experience = new ReactiveProperty<int>(data.Experience);
            
            
            InitCosts(data);
            InitRewards(data);
            InitUnlocks(data);

            Name.Subscribe(newValue => data.Name = newValue);
            Experience.Subscribe(newValue => data.Experience = newValue);
        }

        public bool IsUnlocked(PlayerEntityProxy player)
        {
            foreach (var unlock in Unlocks)
            {
                if (!unlock.IsUnlocked(player))
                {
                    return false;
                }
            }

            return true;
        }

        private void InitCosts(JobData data)
        {
            data.CostResourceTypes.ForEach(type => CostResourceTypes.Add(type));
            
            CostResourceTypes.ObserveAdd().Subscribe(e =>
            {
                var addedType = e.Value;
                data.CostResourceTypes.Add(addedType);
            });

            CostResourceTypes.ObserveRemove().Subscribe(e =>
            {
                var removedType = e.Value;
                data.CostResourceTypes.Remove(removedType);
            });
            
            data.CostResourceValues.ForEach(value => CostResourceValues.Add(value));
            
            CostResourceValues.ObserveAdd().Subscribe(e =>
            {
                var addedValue = e.Value;
                data.CostResourceValues.Add(addedValue);
            });

            CostResourceValues.ObserveRemove().Subscribe(e =>
            {
                var removedValue = e.Value;
                data.CostResourceValues.Remove(removedValue);
            });
        }
        
        private void InitRewards(JobData data)
        {
            data.RewardResourceTypes.ForEach(type => RewardResourceTypes.Add(type));
            
            RewardResourceTypes.ObserveAdd().Subscribe(e =>
            {
                var addedType = e.Value;
                data.RewardResourceTypes.Add(addedType);
            });

            RewardResourceTypes.ObserveRemove().Subscribe(e =>
            {
                var removedType = e.Value;
                data.RewardResourceTypes.Remove(removedType);
            });
            
            data.RewardResourceValues.ForEach(value => RewardResourceValues.Add(value));
            
            RewardResourceValues.ObserveAdd().Subscribe(e =>
            {
                var addedValue = e.Value;
                data.RewardResourceValues.Add(addedValue);
            });

            RewardResourceValues.ObserveRemove().Subscribe(e =>
            {
                var removedValue = e.Value;
                data.RewardResourceValues.Remove(removedValue);
            });
        }
        
        private void InitUnlocks(JobData data)
        {
            data.Unlocks.ForEach(unlock => Unlocks.Add(unlock));
            
            Unlocks.ObserveAdd().Subscribe(e =>
            {
                var addedUnlock = e.Value;
                data.Unlocks.Add(addedUnlock);
            });

            Unlocks.ObserveRemove().Subscribe(e =>
            {
                var removedUnlock = e.Value;
                data.Unlocks.Remove(removedUnlock);
            });
        }
    }
}