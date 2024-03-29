using TMPro;
using UnityEngine;

namespace AnimalsEscape.UI
{
    public class LevelText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;

        public void ShowLevel(int level)
        {
            _levelText.text = $"Level {level}";
        }
    }
}
