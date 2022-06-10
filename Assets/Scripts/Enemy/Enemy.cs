using System.Collections.Generic;
using AnimalsEscape.States;
using UnityEngine;
using UnityEngine.AI;

namespace AnimalsEscape
{
    [RequireComponent(typeof(EnemyAnimations), typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyAnimations _enemyAnimations;
        [SerializeField] private float _idleTime = 3f;
        [field:SerializeField] public Scanner Scanner { get; set; }

        private float _idleTimeChange;
        private float _walkTimeChange;
        private EnemyStateMachine _enemyStateMachine;
        private EnemyIdleState _idleState;
        private EnemyPatrollingState _patrollingState;
        private NavMeshAgent _navMeshAgent;
        public List<Transform> Waypoints { get; set; } = new();

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _idleState = new EnemyIdleState(_enemyAnimations);
            _patrollingState = new EnemyPatrollingState(_enemyAnimations, Waypoints, _navMeshAgent);
            _enemyStateMachine = new EnemyStateMachine();
            _enemyStateMachine.Init(_idleState);
            _idleTimeChange = _idleTime;
        }

        private void Update()
        {
            _enemyStateMachine.CurrentState.Run();
            CheckStates();
        }

        private void CheckStates()
        {
            if (_enemyStateMachine.CurrentState == _patrollingState)
            {
                if (CheckDestinationReached(_navMeshAgent.remainingDistance))
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

        private bool CheckDestinationReached(float dist)
        {
            return !float.IsPositiveInfinity(dist) 
                   && _navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete 
                   && _navMeshAgent.remainingDistance == 0;
        }

        private void SetWalkState()
        {
            _enemyStateMachine.ChangeState(_patrollingState);
            _idleTimeChange = _idleTime;
        }

        private void SetIdleState()
        {
            _enemyStateMachine.ChangeState(_idleState);
        }
    }
}