using System;
using AnimalsEscape.Enums;
using UnityEngine;

namespace AnimalsEscape
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyAnimations _enemyAnimations;
        private EnemyStates currentState;

        private void Start()
        {
            currentState = EnemyStates.Idle;
        }

        private void Update()
        {
            _enemyAnimations.ChangeAnimation(currentState);
        }
    }
}
