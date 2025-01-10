using System.Reflection;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainMenu
{
    public class PlayerBinder : MonoBehaviour
    {
        [SerializeField] private Button _btnDeletePlayer;
        [SerializeField] private Button _btnStartGameplay;
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI playerLevel;
        
        private Subject<Unit> _exitSceneSignalSubj;

        private PlayerViewModel _playerViewModel;
        public void Bind(PlayerViewModel playerViewModel)
        {
            playerName.text = playerViewModel.Name.CurrentValue;
            playerLevel.text = playerViewModel.Level.CurrentValue.ToString();
            
            _playerViewModel = playerViewModel;
        }
        
        protected virtual void Start()
        {
            _btnDeletePlayer?.onClick.AddListener(OnDeletePlayerClick);
            _btnStartGameplay?.onClick.AddListener(OnStartGameplayClick);
        }

        protected virtual void OnDestroy()
        {
            _btnDeletePlayer?.onClick.RemoveListener(OnDeletePlayerClick);
            _btnStartGameplay?.onClick.RemoveListener(OnStartGameplayClick);
        }
        
        private void OnDeletePlayerClick()
        {
            _playerViewModel.RequestDeletePlayer();
        }
        
        private void OnStartGameplayClick()
        {
            _playerViewModel.RequestStartGameplay();
        }
    }
}