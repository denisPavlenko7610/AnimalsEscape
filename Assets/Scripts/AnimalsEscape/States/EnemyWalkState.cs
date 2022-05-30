namespace AnimalsEscape.States
{
    public class EnemyWalkState : IState
    {
        private EnemyAnimations _enemyAnimations;
        public EnemyWalkState(EnemyAnimations enemyAnimations)
        {
            _enemyAnimations = enemyAnimations;
        }
        public void Enter()
        {
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
