using UnityEngine;
using Common.Interfaces;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        #region Unity Fields
        [SerializeField] private int m_maxHealth;
        #endregion Unity Fields

        #region Fields
        private int m_currentHealth;
        #endregion Fields
        
        #region MonoBehaviour Methods
        private void Awake()
        {
            m_currentHealth = m_maxHealth;
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        private void Die()
        {
            
        }
        #endregion Methods
        
        
        #region Damageable Methods
        public void TakeDamage(int damage)
        {
            m_currentHealth -= damage;
            
            if(m_currentHealth <= 0)
                Die();
        }
        #endregion Damageable Methods
    }
}