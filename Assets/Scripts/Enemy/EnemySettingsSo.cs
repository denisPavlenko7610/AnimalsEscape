using System;
using AnimalsEscape.Enums;
using UnityEngine;

namespace AnimalsEscape
{
    [CreateAssetMenu(fileName = "EnemySettingsSo", menuName = "Enemy/EnemySettings")]
    public class EnemySettingsSo : ScriptableObject
    {
        public EnemySetting EnemySettings;
    }
    
    [Serializable]
    public struct EnemySetting
    {
        public EnemyType EnemyType;
        public Enemy EnemyPrefab;
    }
}
