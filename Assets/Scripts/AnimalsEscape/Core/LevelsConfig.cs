using System.Collections.Generic;
using UnityEngine;

namespace AnimalsEscape.Core
{
    [CreateAssetMenu(menuName = "Level/LevelsConfig",fileName = "LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        public List<GameObject> Levels;
    }
}
