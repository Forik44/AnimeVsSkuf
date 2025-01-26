using AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.MainMenuScreen;
using MVVM.UI;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
    public class GameplayScreenBinder : WindowBinder<GameplayScreenViewModel>
    {
       [SerializeField] private Button _btnGoToMainMenu;
       [SerializeField] private Button _btnNextDay;
       [SerializeField] private Button _btnJobsPopup;
       [SerializeField] private TextMeshProUGUI _playerLevel;
       [SerializeField] private TextMeshProUGUI _playerDay;

       protected override void OnBind(GameplayScreenViewModel viewModel)
       {
           base.OnBind(viewModel);
           ViewModel.Level.Subscribe(value => _playerLevel.text = $"Уровень: {value}");
           ViewModel.Day.Subscribe(value => _playerDay.text = $"День: {value}");
       }

       private void OnEnable()
        {
            _btnGoToMainMenu.onClick.AddListener(OnGoToMainMenuClicked);
            _btnNextDay.onClick.AddListener(OnNextDayClicked);
            _btnJobsPopup.onClick.AddListener(OpenJobsPopupClicked);
        }

        private void OnDisable()
        {
            _btnGoToMainMenu.onClick.RemoveListener(OnGoToMainMenuClicked);
            _btnNextDay.onClick.RemoveListener(OnNextDayClicked);
            _btnJobsPopup.onClick.RemoveListener(OpenJobsPopupClicked);
        }
        
        private void OpenJobsPopupClicked()
        {
            ViewModel.RequestOpenPlayersPopup();
        }

        private void OnGoToMainMenuClicked()
        {
            ViewModel.RequestGoToMainMenu();
        }

        private void OnNextDayClicked()
        {
            ViewModel.RequestNextDay();
        }
    }
}