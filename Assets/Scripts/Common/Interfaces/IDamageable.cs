using UnityEngine;

namespace Common.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
        void SpawnDamageEffect(Vector3 atLocation);
    }
}