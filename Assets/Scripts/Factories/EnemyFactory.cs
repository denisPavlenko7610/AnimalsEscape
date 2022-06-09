using System;
using System.Linq;
using AnimalsEscape.Enums;
using UnityEngine;

namespace AnimalsEscape.Factories
{
    public class EnemyFactory : MonoBehaviour, IFactory<Enemy>
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private EnemySettingsSo _enemySettingsSo;

        public Enemy Create()
        {
            foreach (var enemySetting in _enemySettingsSo.EnemySettings.Where(s =>
                s.EnemyType == _enemyType))
            {
                return Instantiate(enemySetting.EnemyPrefab, transform.position, transform.rotation, transform);
            }

            throw new NullReferenceException($"Can`t create {_enemyType}");
        }
    }
}