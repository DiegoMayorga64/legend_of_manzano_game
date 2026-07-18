using UnityEngine;

namespace Project.Player.StateMachine
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(Animator))]
    public class PlayerStateMachine : MonoBehaviour
    {
        public PlayerMovement Movement { get; private set; }
        public Animator Animator { get; private set; }

        public PlayerIdleState IdleState { get; private set; }
        public PlayerRunState RunState { get; private set; }
        public PlayerRollState RollState { get; private set; }
        public PlayerHurtState HurtState { get; private set; }
        public PlayerDeathState DeathState { get; private set; }

        private PlayerState _currentState;

        private void Awake()
        {
            Movement = GetComponent<PlayerMovement>();
            Animator = GetComponent<Animator>();

            IdleState = new PlayerIdleState(this);
            RunState = new PlayerRunState(this);
            RollState = new PlayerRollState(this);
            HurtState = new PlayerHurtState(this);
            DeathState = new PlayerDeathState(this);
        }

        private void Start()
        {
            ChangeState(IdleState);
        }

        private void Update()
        {
            _currentState.Tick();
        }

        public void ChangeState(PlayerState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void TakeHit()
        {
            if (_currentState == DeathState) return; // no interrumpir la muerte
            ChangeState(HurtState);
        }

        public void Die()
        {
            ChangeState(DeathState);
        }
    }
}
