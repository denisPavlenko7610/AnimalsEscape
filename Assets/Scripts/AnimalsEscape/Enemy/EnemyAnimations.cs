using System;
using AnimalsEscape.Enums;
using UnityEngine;

namespace AnimalsEscape
{
    public class EnemyAnimations : MonoBehaviour
    {
        [SerializeField] private Animator _enemyAnimator;
        private static readonly int IsWalk = Animator.StringToHash("IsWalk");

        public void ChangeAnimation(EnemyStates enemyState)
        {
            if (enemyState == EnemyStates.Idle)
            {
                _enemyAnimator.SetBool(IsWalk, false);
            }
            else if (enemyState == EnemyStates.Walk)
            {
                _enemyAnimator.SetBool(IsWalk, true);
            }
            else if (enemyState == EnemyStates.Catch)
            {
            }
        }
    }
}