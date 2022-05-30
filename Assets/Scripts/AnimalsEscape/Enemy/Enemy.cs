using AnimalsEscape.States;
using UnityEngine;

namespace AnimalsEscape
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyAnimations _enemyAnimations;
        [SerializeField] private float _timeToIdle = 3f;

        private float _timeToIdleToChange;
        private EnemyStateMachine _enemyStateMachine;
        private EnemyIdleState _enemyIdleState;
        private EnemyWalkState _enemyWalkState;

        private void Start()
        {
            _enemyIdleState = new EnemyIdleState(_enemyAnimations);
            _enemyWalkState = new EnemyWalkState(_enemyAnimations);
            _enemyStateMachine = new EnemyStateMachine();
            _enemyStateMachine.Init(_enemyIdleState);
            _timeToIdleToChange = _timeToIdle;
        }

        private void Update()
        {
            _enemyStateMachine.CurrentState.Run();

            _timeToIdleToChange -= Time.deltaTime;
            if (_timeToIdleToChange <= 0)
            {
                SetWalkState();
            }
        }

        private void SetWalkState()
        {
            _enemyWalkState.Enter();
            _timeToIdleToChange = _timeToIdle;
        }
    }
}