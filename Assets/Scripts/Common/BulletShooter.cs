using System;
using DG.Tweening;
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
        #region Inspector Fields
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
        
        [Header("Animation")]
        [SerializeField] private GameObject m_shootEffectPrefab;
        [SerializeField] bool m_useSquashAndStretch = true;
        [SerializeField] private Transform m_squashAndStretchTarget;
        #endregion Inspector Fields
        
        #region Fields
        private Vector3 m_initialSquashAndStretchTargetScale;
        #endregion Fields
        
        #region MonoBehaviour Methods

        private void Awake()
        {
            if (m_useSquashAndStretch && m_squashAndStretchTarget != null)
                m_initialSquashAndStretchTargetScale = m_squashAndStretchTarget.localScale;
        }

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

            if (m_useSquashAndStretch && m_squashAndStretchTarget != null)
            {
                Vector3 localScale = m_initialSquashAndStretchTargetScale;
                Vector3 stretch = new Vector3(localScale.x*0.7f, localScale.y*1.3f, localScale.y);
                Vector3 squash = new Vector3(localScale.x*1.14f, localScale.y*0.85f, localScale.y);

                Sequence sequence = DOTween.Sequence();
                sequence.Append(m_squashAndStretchTarget.DOScale(stretch, 0.12f));
                sequence.Append(m_squashAndStretchTarget.DOScale(squash, 0.12f));
                sequence.Append(m_squashAndStretchTarget.DOScale(localScale, 0.1f));
            }
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
