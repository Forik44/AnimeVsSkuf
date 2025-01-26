using System.Collections.Generic;
using MVVM.UI;
using ObservableCollections;
using R3;
using UnityEngine;

namespace AnimeVsSkuf.Scripts.Game.Gameplay.View.Jobs
{
    public class JobsPopupBinder : PopupBinder<JobsPopupViewModel>
    {
        [SerializeField] private JobBinder _prefabJob;
        [SerializeField] private Transform _cardContainer;
    
        private readonly Dictionary<int, JobBinder> _createdJobsMap = new();
        private readonly CompositeDisposable _disposables = new();
        
        private JobsPopupViewModel _jobsPopupViewModel;
        
        protected override void OnBind(JobsPopupViewModel viewModel)
        {
            foreach (var job in viewModel.AllJobs)
            {
                CreateJob(job);
            }
        
            _disposables.Add(viewModel.AllJobs.ObserveAdd()
                .Subscribe(e => CreateJob(e.Value)));
        
            _disposables.Add(viewModel.AllJobs.ObserveRemove()
                .Subscribe(e => DeleteJob(e.Value)));

            _jobsPopupViewModel = viewModel;
        }
        
        protected virtual void Start()
        {
            base.Start();

        }

        private void OnDestroy()
        {
            base.OnDestroy();
            _disposables.Dispose();
        }

        private void CreateJob(JobViewModel jobViewModel)
        {
            var createdJob = Instantiate(_prefabJob, _cardContainer);
            createdJob.Bind(jobViewModel);
        
            _createdJobsMap[jobViewModel.JobId] = createdJob;
        }

        private void DeleteJob(JobViewModel jobViewModel)
        {
            if (_createdJobsMap.TryGetValue(jobViewModel.JobId, out var jobBinder))
            {
                Destroy(jobBinder.gameObject);
                _createdJobsMap.Remove(jobViewModel.JobId);
            }
        }
    }
}