using AnimalsEscape.States;
using UnityEngine;

namespace AnimalsEscape
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyAnimations _enemyAnimations;
        [SerializeField] private float _idleTime = 3f;
        [SerializeField] private float _walkTime = 3f;

        private float _idleTimeChange;
        private float _walkTimeChange;
        private EnemyStateMachine _enemyStateMachine;
        private EnemyIdleState _idleState;
        private EnemyWalkState _walkState;

        private void Start()
        {
            _idleState = new EnemyIdleState(_enemyAnimations);
            _walkState = new EnemyWalkState(_enemyAnimations);
            _enemyStateMachine = new EnemyStateMachine();
            _enemyStateMachine.Init(_idleState);
            _idleTimeChange = _idleTime;
            _walkTimeChange = _walkTime;
        }

        private void Update()
        {
            _enemyStateMachine.CurrentState.Run();
            CheckStates();
        }

        private void CheckStates()
        {
            if (_enemyStateMachine.CurrentState == _walkState)
            {
                _walkTimeChange -= Time.deltaTime;
                if (_walkTimeChange <= 0)
                {
                    SetIdleState();
                }
            }
            else if (_enemyStateMachine.CurrentState == _idleState)
            {
                {
                    _idleTimeChange -= Time.deltaTime;
                    if (_idleTimeChange <= 0)
                    {
                        SetWalkState();
                    }
                }
            }
        }

        private void SetWalkState()
        {
            _enemyStateMachine.ChangeState(_walkState);
            _idleTimeChange = _idleTime;
        }

        private void SetIdleState()
        {
            _enemyStateMachine.ChangeState(_idleState);
            _walkTimeChange = _walkTime;
        }
    }
}