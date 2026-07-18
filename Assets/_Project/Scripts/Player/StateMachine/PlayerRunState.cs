using UnityEngine;

namespace Project.Player.StateMachine
{
    public class PlayerRunState : PlayerState
    {
        public PlayerRunState(PlayerStateMachine sm) : base(sm) { }

        public override void Enter() => StateMachine.Animator.Play("Run");

        public override void Tick()
        {
            var m = StateMachine.Movement;
            if (!m.IsGrounded) StateMachine.ChangeState(StateMachine.RollState);
            else if (!m.IsMoving) StateMachine.ChangeState(StateMachine.IdleState);
        }

        public override void Exit() { }
    }
}
