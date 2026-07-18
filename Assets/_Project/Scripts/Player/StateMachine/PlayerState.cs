namespace Project.Player.StateMachine
{
    public abstract class PlayerState
    {
        protected readonly PlayerStateMachine StateMachine;

        protected PlayerState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void Enter();
        public abstract void Tick();
        public abstract void Exit();
    }
}