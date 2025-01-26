using AnimeVsSkuf.Scripts.Game.Gameplay.Services;
using MVVM.UI;
using ObservableCollections;

namespace AnimeVsSkuf.Scripts.Game.Gameplay.View.Jobs
{
    public class JobsPopupViewModel : WindowViewModel
    {
        public override string Id => "Gameplay/Job/JobsPopup";
        
        public readonly IObservableCollection<JobViewModel> AllJobs;
        private readonly JobsService _jobService;
        
        public JobsPopupViewModel(JobsService jobService)
        {
            _jobService = jobService;
            AllJobs = jobService.Jobs;
        }
    }
}