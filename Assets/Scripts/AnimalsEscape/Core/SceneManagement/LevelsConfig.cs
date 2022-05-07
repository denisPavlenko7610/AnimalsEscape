using System.Collections.Generic;
using UnityEngine;

namespace AnimalsEscape.Core.SceneManagement
{
    [CreateAssetMenu(menuName = "Levels/LevelsConfig", fileName = "LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        public List<Object> Levels;
    }
}
