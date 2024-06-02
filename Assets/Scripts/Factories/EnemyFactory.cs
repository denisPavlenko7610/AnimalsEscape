using System.Linq;
using AnimalsEscape.Enums;
using RDTools;
using System.Collections.Generic;
using UnityEngine;

namespace AnimalsEscape.Factories
{
    public class EnemyFactory : MonoBehaviour, IFactory<Enemy>
    {
        [SerializeField] EnemyType _enemyType;
        [SerializeField] EnemySettingsSo _enemySettingsSo;
        [SerializeField] List<EnemyStartPoint> _enemyStartPoints = new();

        [Button]
        void FindEnemyStartPoints()
        {
            _enemyStartPoints = FindObjectsByType<EnemyStartPoint>(FindObjectsSortMode.None).ToList();
        }

        public Enemy Create()
        {
            foreach (EnemyStartPoint point in _enemyStartPoints)
            {
                return Instantiate(point.enemySetting.EnemySettings.EnemyPrefab, point.transform.position, point.transform.rotation, transform);
            }

            Debug.LogError($"Can`t create {_enemyType}");
            return null;
        }
    }
}