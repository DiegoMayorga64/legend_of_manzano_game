using UnityEngine;
namespace Project.Gameplay
{
    public interface IDamageable
    {
        void TakeDamage(int amount, Vector2 hitDirection);
    }
}