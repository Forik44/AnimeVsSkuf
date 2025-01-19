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
       [SerializeField] private TextMeshProUGUI _playerLevel;

       protected override void OnBind(GameplayScreenViewModel viewModel)
       {
           base.OnBind(viewModel);
           ViewModel.Level.Subscribe(value => _playerLevel.text = $"Уровень: {value}");
       }

       private void OnEnable()
        {
            _btnGoToMainMenu.onClick.AddListener(OnGoToMainMenuClicked);
            _btnNextDay.onClick.AddListener(OnNextDayClicked);
        }

        private void OnDisable()
        {
            _btnGoToMainMenu.onClick.RemoveListener(OnGoToMainMenuClicked);
            _btnNextDay.onClick.RemoveListener(OnNextDayClicked);
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