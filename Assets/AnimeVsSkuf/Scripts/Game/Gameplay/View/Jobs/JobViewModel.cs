using System.Collections.Generic;
using AnimeVsSkuf.Scripts.Game.Gameplay.Services;
using Game.State;
using Game.State.GameResources;
using Game.State.Jobs;
using Game.State.Unlock;
using MVVM.UI;
using ObservableCollections;
using R3;

namespace AnimeVsSkuf.Scripts.Game.Gameplay.View.Jobs
{
    public class JobViewModel : WindowViewModel
    {
        public override string Id => "Gameplay/Job/JobCard";
        
        private readonly JobsService _jobsService;
        private readonly PlayerEntityProxy _player;
        public readonly int JobId;
        public readonly ReactiveProperty<string> Name;
        public readonly ObservableList<ResourceType> CostResourceTypes;
        public readonly ObservableList<double> CostResourceValues;
        public readonly ObservableList<ResourceType> RewardResourceTypes;
        public readonly ObservableList<double> RewardResourceValues;
        public readonly ObservableList<IUnlockData> Unlocks;
        public readonly ReactiveProperty<int> Experience;

        public Subject<Unit> RequestUpdateCardsSignal;

        private readonly Job _job;

        public JobViewModel(Job job, JobsService jobsService, PlayerEntityProxy player)
        {
            _jobsService = jobsService;
            _player = player;
            _job = job;
            
            JobId = job.Id;
            Name = job.Name;
            CostResourceTypes = job.CostResourceTypes;
            CostResourceValues = job.CostResourceValues;
            RewardResourceTypes = job.RewardResourceTypes;
            RewardResourceValues = job.RewardResourceValues;
            Unlocks = job.Unlocks;
            Experience = job.Experience;
            RequestUpdateCardsSignal = new Subject<Unit>();
        }

        public void RequestGoToJob()
        {
            if (IsUnlocked() && IsResourcesEnough())
                _jobsService.GoToJob(JobId);
        }

        public bool IsUnlocked()
        {
            return _job.IsUnlocked(_player);
        }

        public bool IsResourcesEnough()
        {
            return _jobsService.IsJobResourceEnough(JobId);
        }

        public void RequestUpdateCard()
        {
            RequestUpdateCardsSignal.OnNext(Unit.Default);
        }
    }
}