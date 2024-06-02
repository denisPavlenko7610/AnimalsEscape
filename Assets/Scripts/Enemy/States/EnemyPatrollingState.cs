using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AnimalsEscape.States
{
    public class EnemyPatrollingState : IState
    {
        EnemyAnimations _enemyAnimations;
        List<Transform> _waypoints = new();
        NavMeshAgent _navMeshAgent;

        public EnemyPatrollingState(EnemyAnimations enemyAnimations, List<Transform> waypoints,
            NavMeshAgent navMeshAgent)
        {
            _enemyAnimations = enemyAnimations;
            _waypoints = waypoints;
            _navMeshAgent = navMeshAgent;
        }

        public void Enter()
        {
            int randomIndex = Random.Range(0, _waypoints.Count);
            _navMeshAgent.SetDestination(_waypoints[randomIndex].position);
            _enemyAnimations.PlayWalk();
        }

        public void Exit()
        {
        }

        public void Run()
        {
        }
    }
}