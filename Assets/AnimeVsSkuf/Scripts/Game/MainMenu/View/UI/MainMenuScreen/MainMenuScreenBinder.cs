using MVVM.UI;
using UnityEngine;
using UnityEngine.UI;

namespace AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.MainMenuScreen
{
    public class MainMenuScreenBinder : WindowBinder<MainMenuScreenViewModel>
    {
        [SerializeField] private Button _btnOpenPlayersPopup;

        private void OnEnable()
        {
            _btnOpenPlayersPopup.onClick.AddListener(OnOpenPlayersPopupClicked);
        }

        private void OnDisable()
        {
            _btnOpenPlayersPopup.onClick.RemoveListener(OnOpenPlayersPopupClicked);
        }

        private void OnOpenPlayersPopupClicked()
        {
            ViewModel.RequestOpenPlayersPopup();
        }
    }
}