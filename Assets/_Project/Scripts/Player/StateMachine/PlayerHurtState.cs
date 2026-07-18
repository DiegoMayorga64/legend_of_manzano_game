using UnityEngine;
namespace Project.Player.StateMachine
{
public class PlayerHurtState : PlayerState
{
    private float _hurtDuration = 0.4f; // ajustable según el largo real del clip
    private float _timer;

    public PlayerHurtState(PlayerStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        StateMachine.Animator.Play("Hit");
        _timer = _hurtDuration;
    }

    public override void Tick()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            var m = StateMachine.Movement;
            StateMachine.ChangeState(m.IsGrounded
                ? (m.IsMoving ? StateMachine.RunState : StateMachine.IdleState)
                : StateMachine.RollState);
        }
    }

    public override void Exit() { }
}
}