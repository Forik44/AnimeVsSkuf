using System.Collections.Generic;
using AnimeVsSkuf.Scripts.Game.Gameplay.Commands;
using AnimeVsSkuf.Scripts.Game.Gameplay.View.Jobs;
using Game.GameResources;
using Game.State;
using Game.State.CMD;
using Game.State.GameResources;
using Game.State.Jobs;
using ObservableCollections;
using R3;

namespace AnimeVsSkuf.Scripts.Game.Gameplay.Services
{
    public class JobsService
    {
        public readonly ObservableList<JobViewModel> Jobs = new();

        private readonly Dictionary<int, JobViewModel> _jobsMap = new();
        private readonly ICommandProcessor _cmd;
        private readonly PlayerEntityProxy _player;
        private readonly ResourcesService _resourcesService;

        public JobsService(ObservableList<Job> jobs, ICommandProcessor cmd, PlayerEntityProxy player, ResourcesService resourcesService)
        {
            _cmd = cmd;
            _player = player;
            _resourcesService = resourcesService;
            jobs.ForEach(CreateJobViewModel);
            jobs.ObserveAdd().Subscribe(e => CreateJobViewModel(e.Value));
            jobs.ObserveRemove().Subscribe(e => RemoveJobViewModel(e.Value));
        }

        public bool GoToJob(int jobId)
        {
            var command = new CmdGoToJob(jobId);
            
            bool result = _cmd.Process(command);

            foreach (var job in _jobsMap)
            {
                job.Value.RequestUpdateCard();
            }

            return result;
        }
        
        public bool IsJobResourceEnough(int jobId)
        {
            _jobsMap.TryGetValue(jobId, out var jobViewModel);
            for (int i = 0; i < jobViewModel.CostResourceTypes.Count; i++)
            {
                if (!_resourcesService.IsEnoughResources(jobViewModel.CostResourceTypes[i], jobViewModel.CostResourceValues[i]))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        private void CreateJobViewModel(Job job)
        {
            var jobViewModel = new JobViewModel(job, this, _player);
            _jobsMap[jobViewModel.JobId] = jobViewModel;
            
            Jobs.Add(jobViewModel);
        }
        
        private void RemoveJobViewModel(Job job)
        {
            if (_jobsMap.TryGetValue(job.Id, out var jobViewModel))
            {
                Jobs.Remove(jobViewModel);
                _jobsMap.Remove(job.Id);
            }
        }
    }
}