using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsEscape.UI
{
    public class LevelText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _levelText;
        [SerializeField] Button _videoButton;

        public void ShowLevel(int level)
        {
            _levelText.text = $"Level {level}";
        }
        
        public Button GetVideoButton => _videoButton;
    }
}
