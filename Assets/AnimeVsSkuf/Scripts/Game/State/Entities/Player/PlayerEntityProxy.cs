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
        public ObservableList<Resource> Resources { get; } = new();
        
        public PlayerEntity Origin { get; }

        public PlayerEntityProxy(PlayerEntity playerEntity)
        {
            Id = playerEntity.Id;
            
            Name = new ReactiveProperty<string>(playerEntity.Name);
            Level = new ReactiveProperty<int>(playerEntity.Level);
            
            InitResources(playerEntity);
            
            Origin = playerEntity;
            
            Name.Skip(1).Subscribe(value => playerEntity.Name = value);
            Level.Skip(1).Subscribe(value => playerEntity.Level = value);
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