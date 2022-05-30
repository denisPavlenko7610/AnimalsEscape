using AnimalsEscape.Enums;

namespace AnimalsEscape.States
{
    public class EnemyStateMachine
    {
        public IState CurrentState;

        public void Init(IState startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(IState newState)
        {   
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
