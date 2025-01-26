using System;
using System.Collections.Generic;
using AnimeVsSkuf.Scripts.Game.Gameplay.Commands;
using Game.GameResources;
using Game.State.CMD;
using Game.State.GameResources;
using ObservableCollections;
using R3;
using UnityEngine;

namespace AnimeVsSkuf.Scripts.Game.Gameplay.Services
{
    public class ResourcesService
    {
        public readonly ObservableList<ResourceViewModel> Resources = new();

        private readonly Dictionary<ResourceType, ResourceViewModel> _resourcesMap = new();
        private readonly ICommandProcessor _cmd;

        public ResourcesService(ObservableList<Resource> resources, ICommandProcessor cmd)
        {
            _cmd = cmd;
            resources.ForEach(CreateResourceViewModel);
            resources.ObserveAdd().Subscribe(e => CreateResourceViewModel(e.Value));
            resources.ObserveRemove().Subscribe(e => RemoveResourceViewModel(e.Value));
        }

        public bool AddResource(ResourceType resourceType, double amount, bool canClamp = true)
        {
            Debug.Log($"You try add {resourceType} - {amount}");
            var command = new CmdResourcesAdd(resourceType, amount, canClamp);
            
            return _cmd.Process(command);
        }
        
        public bool TrySpendResource(ResourceType resourceType, double amount, bool canClamp = false)
        {
            Debug.Log($"You try spend {resourceType} - {amount}");
            var command = new CmdResourcesSpend(resourceType, amount, canClamp);
            
            return _cmd.Process(command);
        }
        
        public bool SetResource(ResourceType resourceType, double amount, bool canClamp = false)
        {
            Debug.Log($"You try set {resourceType} - {amount}");
            var command = new CmdResourcesSet(resourceType, amount, canClamp);
            
            return _cmd.Process(command);
        }

        public bool IsEnoughResources(ResourceType resourceType, double amount)
        {
            if (_resourcesMap.TryGetValue(resourceType, out var resourceViewModel))
            {
                return resourceViewModel.Amount.CurrentValue - resourceViewModel.MinValue.CurrentValue >= amount;
            }

            return false;
        }
        
        public Observable<double> ObserveResource(ResourceType resourceType)
        {
            if (_resourcesMap.TryGetValue(resourceType, out var resourceViewModel))
            {
                return resourceViewModel.Amount;
            }

            throw new Exception($"Resource of type {resourceType} doesn't exist");
        }
        private void RemoveResourceViewModel(Resource resource)
        {
            if (_resourcesMap.TryGetValue(resource.ResourceType, out var resourceViewModel))
            {
                Resources.Remove(resourceViewModel);
                _resourcesMap.Remove(resource.ResourceType);
            }
        }

        private void CreateResourceViewModel(Resource resource)
        {
            var resourceViewModel = new ResourceViewModel(resource);
            _resourcesMap[resourceViewModel.ResourceType] = resourceViewModel;
            
            Resources.Add(resourceViewModel);
        }
    }
}