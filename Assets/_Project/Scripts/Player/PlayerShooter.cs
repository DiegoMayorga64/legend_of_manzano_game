using UnityEngine;

using UnityEngine;
using Project.Gameplay;

namespace Project.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private GameObject _applePrefab;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _shootCooldown = 0.3f;

        private PlayerInputHandler _input;
        private PlayerMovement _movement;
        private bool _canShoot = true;

        private void Awake()
        {
            _input = GetComponent<PlayerInputHandler>();
            _movement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (_input.ShootPressed && _canShoot)
            {
                Shoot();
            }
            _input.ConsumeShootFlag();
        }

        private void Shoot()
        {
            _canShoot = false;

            Vector2 direction = _movement.FacingRight ? Vector2.right : Vector2.left;
            GameObject appleObj = Instantiate(_applePrefab, _shootPoint.position, Quaternion.identity);
            appleObj.GetComponent<Apple>().Initialize(direction);

            Invoke(nameof(ResetShoot), _shootCooldown);
        }

        private void ResetShoot() => _canShoot = true;
    }
}