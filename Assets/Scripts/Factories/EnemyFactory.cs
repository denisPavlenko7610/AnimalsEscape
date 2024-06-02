using System.Linq;
using AnimalsEscape.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace AnimalsEscape.Factories
{
    public class EnemyFactory : MonoBehaviour, IFactory<Enemy>
    {
        [SerializeField] EnemyType _enemyType;
        [SerializeField] EnemySettingsSo _enemySettingsSo;
        [SerializeField] List<EnemyStartPoint> _enemyStartPoints = new();

        void OnValidate()
        {
            if (_enemyStartPoints.Count == 0)
            {
                _enemyStartPoints = FindObjectsByType<EnemyStartPoint>(FindObjectsSortMode.None).ToList();
            }
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