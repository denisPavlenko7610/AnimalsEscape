using System;
using System.Collections.Generic;
using AnimalsEscape.Enums;
using UnityEngine;

namespace AnimalsEscape
{
    [CreateAssetMenu(fileName = "EnemySettingsSo", menuName = "Enemy/EnemySettings")]
    public class EnemySettingsSo : ScriptableObject
    {
        public List<EnemySetting> EnemySettings;
    }
    
    [Serializable]
    public struct EnemySetting
    {
        public EnemyType EnemyType;
        public Enemy EnemyPrefab;
    }
}
