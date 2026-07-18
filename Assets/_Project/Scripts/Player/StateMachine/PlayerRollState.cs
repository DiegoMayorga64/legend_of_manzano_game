using UnityEngine;

namespace Project.Player.StateMachine
{
public class PlayerRollState : PlayerState
{
    public PlayerRollState(PlayerStateMachine sm) : base(sm) { }
    public override void Enter() => StateMachine.Animator.Play("Roll");

    public override void Tick()
    {
        var m = StateMachine.Movement;
        if (m.IsGrounded)
        {
            StateMachine.ChangeState(m.IsMoving ? StateMachine.RunState : StateMachine.IdleState);
        }
    }

    public override void Exit() { }
}
}
