using AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.MainMenuScreen;
using MVVM.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
    public class GameplayScreenBinder : WindowBinder<GameplayScreenViewModel>
    {
       [SerializeField] private Button _btnGoToMainMenu;

        private void OnEnable()
        {
            _btnGoToMainMenu.onClick.AddListener(OnGoToMainMenuClicked);
        }

        private void OnDisable()
        {
            _btnGoToMainMenu.onClick.RemoveListener(OnGoToMainMenuClicked);
        }

        private void OnGoToMainMenuClicked()
        {
            ViewModel.RequestGoToMainMenu();
        }
    }
}