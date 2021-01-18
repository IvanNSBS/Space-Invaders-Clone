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
        
        [Header("Collision")]
        [SerializeField] private LayerMask m_bulletCollisionLayer;
        [SerializeField] private GameObject m_collisionFX;
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
                float verticalDirection = GetBulletVerticalDirection(m_bulletDirection);
                bullet.StartBullet(m_bulletCollisionLayer, m_bulletSpeed, m_bulletDamage, verticalDirection, m_color, SpawnCollisionEffect);
            }
        }
        #endregion Methods
        
        
        #region Utility Methods
        private void SpawnCollisionEffect(Vector3 collisionLocation)
        {
            var effect = Instantiate(m_collisionFX);
            effect.transform.position = collisionLocation;
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
