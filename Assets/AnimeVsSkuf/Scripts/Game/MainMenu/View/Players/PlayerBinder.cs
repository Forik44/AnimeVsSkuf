using System.Reflection;
using TMPro;
using UnityEngine;

namespace Game.MainMenu
{
    public class PlayerBinder : MonoBehaviour
    {
        public TextMeshProUGUI playerName;
        public TextMeshProUGUI playerLevel;
        public void Bind(PlayerViewModel playerViewModel)
        {
            playerName.text = playerViewModel.Name.CurrentValue;
            playerLevel.text = playerViewModel.Level.CurrentValue.ToString();
        }
    }
}