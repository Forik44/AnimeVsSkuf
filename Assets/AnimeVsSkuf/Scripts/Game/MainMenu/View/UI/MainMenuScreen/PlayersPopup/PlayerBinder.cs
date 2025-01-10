using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainMenu
{
    public class PlayerBinder : MonoBehaviour
    {
        [SerializeField] private Button _btnDeletePlayer;
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI playerLevel;

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
        }

        protected virtual void OnDestroy()
        {
            _btnDeletePlayer?.onClick.RemoveListener(OnDeletePlayerClick);
        }
        
        protected void OnDeletePlayerClick()
        {
            _playerViewModel.DeletePlayer();
        }
    }
}