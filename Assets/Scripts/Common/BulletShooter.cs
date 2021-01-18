using UnityEngine;

namespace Common
{
    public enum BulletDirection
    {
        Up,
        Down
    }
    
    #pragma warning disable CS0649
    public class BulletShooter : MonoBehaviour
    {
        #region Fields
        [Header("Damage")]
        [SerializeField] private int m_bulletDamage;
        
        [Header("Movement")]
        [SerializeField] private float m_bulletSpeed;
        [SerializeField] private BulletDirection m_bulletDirection;

        [Header("Instantiation")]
        [SerializeField] private Color m_color;
        [SerializeField] private GameObject m_bulletPrefab;
        [SerializeField] private Transform m_spawnBulletLocation;
        [SerializeField] private GameObject m_shootEffectPrefab;

        
        [Header("Collision")]
        [SerializeField] private LayerMask m_bulletCollisionLayer;
        #endregion Fields
        
        
        #region MonoBehaviour Methods
        #endregion MonoBehaviour Methodsi
        
        
        #region Methods
        public void Shoot()
        {
            var bulletGameObject = Instantiate(m_bulletPrefab);
            bulletGameObject.transform.position = m_spawnBulletLocation.position;
            
            Bullet bullet = bulletGameObject.GetComponent<Bullet>();
            if (bullet == null)
            {
                Destroy(bulletGameObject);
            }
            else
            {
                SpawnShootEffect();
                float verticalDirection = GetBulletVerticalDirection(m_bulletDirection);
                bullet.StartBullet(m_bulletCollisionLayer, m_bulletSpeed, m_bulletDamage, verticalDirection, m_color);
            }
        }
        #endregion Methods
        
        
        #region Utility Methods
        private void SpawnShootEffect()
        {
            var effect = Instantiate(m_shootEffectPrefab);
            effect.transform.position = m_spawnBulletLocation.position;
        }
        
        private float GetBulletVerticalDirection(BulletDirection direction)
        {
            if (direction == BulletDirection.Down)
                return -1;

            return 1;
        }
        #endregion Utility Methods
    }
}
