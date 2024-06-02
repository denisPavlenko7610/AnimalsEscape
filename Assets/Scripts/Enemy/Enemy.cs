using System.Collections.Generic;
using AnimalsEscape.States;
using RDTools.AutoAttach;
using UnityEngine;
using UnityEngine.AI;

namespace AnimalsEscape
{
    [RequireComponent(typeof(EnemyAnimations), typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] EnemyAnimations _enemyAnimations;
        [SerializeField] float _idleTime = 3f;
        [field:SerializeField, Attach(Attach.Default,false)] public FieldOfView FieldOfView { get; private set; }
        [SerializeField] NavMeshAgent _navMeshAgent;
        
        float _idleTimeChange;
        float _walkTimeChange;
        EnemyStateMachine _enemyStateMachine;
        EnemyIdleState _idleState;
        EnemyPatrollingState _patrollingState;
        
        
        public void Init(List<Transform> waypoints)
        {
            _idleState = new EnemyIdleState(_enemyAnimations);
            _patrollingState = new EnemyPatrollingState(_enemyAnimations, waypoints, _navMeshAgent);
            _enemyStateMachine = new EnemyStateMachine();
            _enemyStateMachine.Init(_idleState);
            _idleTimeChange = _idleTime;
        }

        void Update()
        {
            _enemyStateMachine.CurrentState.Run();
            CheckStates();
        }

        void CheckStates()
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

        bool CheckDestinationReached(float dist)
        {
            return !float.IsPositiveInfinity(dist) 
                   && _navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete 
                   && _navMeshAgent.remainingDistance == 0;
        }

        void SetWalkState()
        {
            _enemyStateMachine.ChangeState(_patrollingState);
            _idleTimeChange = _idleTime;
        }

        void SetIdleState()
        {
            _enemyStateMachine.ChangeState(_idleState);
        }
    }
}