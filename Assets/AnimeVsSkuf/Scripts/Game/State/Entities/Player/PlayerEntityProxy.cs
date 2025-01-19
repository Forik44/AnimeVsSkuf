using System.Collections.Generic;
using System.Linq;
using Game.State.GameResources;
using ObservableCollections;
using R3;
using UnityEngine;

namespace Game.State
{
    public class PlayerEntityProxy
    {
        public int Id { get; }
        public ReactiveProperty<string> Name { get; }
        public ReactiveProperty<int> Level { get; }
        public ReactiveProperty<int> Experience { get; }
        public List<int> LevelUpgrades { get; }
        public ReactiveProperty<int> Day { get; }
        public ObservableList<Resource> Resources { get; } = new();
        
        public PlayerEntity Origin { get; }

        public PlayerEntityProxy(PlayerEntity playerEntity)
        {
            Id = playerEntity.Id;
            LevelUpgrades = playerEntity.LevelUpgrades;
            
            Name = new ReactiveProperty<string>(playerEntity.Name);
            Level = new ReactiveProperty<int>(playerEntity.Level);
            Experience = new ReactiveProperty<int>(playerEntity.Experience);
            Day = new ReactiveProperty<int>(playerEntity.Day);
            
            InitResources(playerEntity);
            
            Origin = playerEntity;
            
            Name.Skip(1).Subscribe(value => playerEntity.Name = value);
            Level.Skip(1).Subscribe(value => playerEntity.Level = value);
            Experience.Skip(1).Subscribe(value =>
                {
                    playerEntity.Experience = value;
                    Level.Value = GetLevelByExperience(value);
                }
            );
            Day.Skip(1).Subscribe(value => playerEntity.Day = value);
        }

        private int GetLevelByExperience(int value)
        {
            int level = 1;
            foreach (var levelUpgrade in LevelUpgrades)
            {
                if (levelUpgrade > value)
                {
                    return level;
                }

                level++;
            }
            return level;
        }

        private void InitResources(PlayerEntity playerEntity)
        {
            playerEntity.Resources.ForEach(resourceData => Resources.Add(new Resource(resourceData)));
            
            Resources.ObserveAdd().Subscribe(e =>
            {
                var addedResource = e.Value;
                playerEntity.Resources.Add(addedResource.Origin);
            });

            Resources.ObserveRemove().Subscribe(e =>
            {
                var removedResource = e.Value;
                var removedResourceData =
                    playerEntity.Resources.FirstOrDefault(resourceData => resourceData.ResourceType == removedResource.ResourceType);
                playerEntity.Resources.Remove(removedResourceData);
            });
        }
    }
}