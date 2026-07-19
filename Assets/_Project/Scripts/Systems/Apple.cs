using UnityEngine;

namespace Project.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Apple : MonoBehaviour
    {
        [SerializeField] private float _speed = 100f;
        [SerializeField] private float _lifetime = 2f;
        [SerializeField] private LayerMask _groundLayer;

        [Header("Visual")]

        [Header("Combat")]
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _spinSpeed = 360f;

        private Vector2 _direction;
        private float _timer;

        public void Initialize(Vector2 direction)
        {
            _direction = direction;
        }

        private void Start()
        {
            _timer = _lifetime;
        }

        private void Update()
        {
            transform.position += (Vector3)(_direction * _speed * Time.deltaTime);
            transform.Rotate(0f, 0f, _spinSpeed * Time.deltaTime); // ← línea nueva

            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                Vector2 hitDirection = ((Vector2)(other.transform.position - transform.position)).normalized;
                damageable.TakeDamage(1, hitDirection);
                Destroy(gameObject);
                return;
            }

            if (((1 << other.gameObject.layer) & _groundLayer) != 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
