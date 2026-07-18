using UnityEngine;

namespace Project.Player.StateMachine
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(PlayerStateMachine sm) : base(sm) { }

        public override void Enter() => StateMachine.Animator.Play("Idle");

        public override void Tick()
        {
            var m = StateMachine.Movement;
            if (!m.IsGrounded) StateMachine.ChangeState(StateMachine.RollState);
            else if (m.IsMoving) StateMachine.ChangeState(StateMachine.RunState);
        }

        public override void Exit() { }
    }
}
