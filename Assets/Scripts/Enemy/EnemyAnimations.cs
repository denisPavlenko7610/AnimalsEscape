using UnityEngine;

namespace AnimalsEscape
{
    public class EnemyAnimations : MonoBehaviour
    {
        [SerializeField] private Animator _enemyAnimator;
        private static readonly int IsWalk = Animator.StringToHash("IsWalk");

        public void PlayIdle() => _enemyAnimator.SetBool(IsWalk, false);
        public void PlayWalk() => _enemyAnimator.SetBool(IsWalk, true);
    }
}