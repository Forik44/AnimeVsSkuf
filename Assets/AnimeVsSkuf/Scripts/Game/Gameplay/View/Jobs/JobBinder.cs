using System;
using MVVM.UI;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AnimeVsSkuf.Scripts.Game.Gameplay.View.Jobs
{
    public class JobBinder : PopupBinder<JobViewModel>
    {
        [SerializeField] private Button _btnGoToJob;
        [SerializeField] private TextMeshProUGUI _jobName;
        [SerializeField] private Image _imageUnlocked;
        [SerializeField] private Image _imageUnlockedButNotEnoughtResources;
        [SerializeField] private Image _imageLocked;
        
        private JobViewModel _jobViewModel;
        private readonly CompositeDisposable _disposables = new();
        
        protected override void OnBind(JobViewModel viewModel)
        {
            _jobViewModel = viewModel;
            _jobName.text = viewModel.Name.Value;
            
            UpdateCardImage();
            _disposables.Add(viewModel.RequestUpdateCardsSignal.Subscribe(e => UpdateCardImage()));
            
        }

        protected virtual void Start()
        {
            base.Start();
            
            _btnGoToJob?.onClick.AddListener(OnGoToJobClick);
        }
        
        private void OnDestroy()
        {
            base.OnDestroy();
            _disposables.Dispose();
            _btnGoToJob?.onClick.RemoveListener(OnGoToJobClick);
        }
        
        private void OnGoToJobClick()
        {
            _jobViewModel.RequestGoToJob();
        }
        
        private void UpdateCardImage()
        {
            _imageUnlocked.gameObject.SetActive(_jobViewModel.IsUnlocked() && _jobViewModel.IsResourcesEnough());
            _imageUnlockedButNotEnoughtResources.gameObject.SetActive(_jobViewModel.IsUnlocked() && !_jobViewModel.IsResourcesEnough());
            _imageLocked.gameObject.SetActive(!_jobViewModel.IsUnlocked());
        }
    }
}