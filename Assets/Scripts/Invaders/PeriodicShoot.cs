using Common;
using UnityEngine;

namespace Invaders
{
    public class PeriodicShoot : MonoBehaviour
    {
        #region Fields
        private Vector2 m_delayRange;
        private float m_currentCooldown;
        private BulletShooter m_shooter;
        #endregion Fields


        #region MonoBehaviour Methods
        private void Update()
        {
            if (m_shooter)
            {
                m_currentCooldown -= Time.deltaTime;
                if (m_currentCooldown <= 0)
                {
                    m_shooter.Shoot();
                    m_currentCooldown = Random.Range(m_delayRange.x, m_delayRange.y);   
                }
            }
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        public void Initialize(Vector2 delayRange)
        {
            m_delayRange = delayRange;
            m_currentCooldown = Random.Range(delayRange.x, delayRange.y);   
            m_shooter = GetComponent<BulletShooter>();
        }
        #endregion Methods
    }
}