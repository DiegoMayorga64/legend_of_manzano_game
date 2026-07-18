using UnityEngine;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInputHandler))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _speed = 150f;
        [SerializeField] private float _jumpVelocity = 300f;

        private SpriteRenderer _spriteRenderer;

        [Header("Ground Check")]
        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _jumpSound;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundCheckRadius = 0.1f;
        [SerializeField] private LayerMask _groundLayer;

        private Rigidbody2D _rb;
        private PlayerInputHandler _input;

        public Rigidbody2D Rigidbody => _rb;

        public bool IsGrounded { get; private set; }
        public bool IsMoving { get; private set; }
        public bool FacingRight { get; private set; } = true;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _input = GetComponent<PlayerInputHandler>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            CheckGrounded();
            HandleJump();
            HandleHorizontalMovement();
            _input.ConsumeJumpFlags();
        }

        private void CheckGrounded()
        {
            IsGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        }

        private void HandleJump()
        {
            if (_input.JumpPressed && IsGrounded)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpVelocity);
                _audioSource.PlayOneShot(_jumpSound);
            }
        }

        private void HandleHorizontalMovement()
        {
            float direction = _input.MoveInput.x;
            IsMoving = Mathf.Abs(direction) > 0.01f;

            if (IsMoving)
            {
                bool newFacingRight = direction > 0;
                if (newFacingRight != FacingRight)
                {
                    FacingRight = newFacingRight;
                    _spriteRenderer.flipX = !FacingRight;
                }
                _rb.linearVelocity = new Vector2(direction * _speed, _rb.linearVelocity.y);
            }
            else
            {
                float newX = Mathf.MoveTowards(_rb.linearVelocity.x, 0, _speed * Time.fixedDeltaTime);
                _rb.linearVelocity = new Vector2(newX, _rb.linearVelocity.y);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_groundCheck == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        }
    }
}