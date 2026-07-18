using UnityEngine;

namespace Project.Player.StateMachine
{
public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(PlayerStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        StateMachine.Animator.Play("Death");
        // Acá eventualmente: deshabilitar input, mostrar pantalla de game over, etc.
    }

    public override void Tick() { } // estado terminal, no hay transición automática de salida

    public override void Exit() { }
}
}
