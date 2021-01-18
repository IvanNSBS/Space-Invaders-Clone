using UnityEngine;
using Common.Interfaces;

namespace Common
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class EntityHealth : MonoBehaviour, IDamageable
    {
        #region Inspector Fields
        [SerializeField] private int m_maxHealth;
        [SerializeField] private GameObject m_damageTakenEffect;
        #endregion Inspector Fields

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
        protected abstract void Die();
        protected abstract void DamageTakenAnimation();
        #endregion Methods
        
        
        #region Damageable Methods
        public void TakeDamage(int damage)
        {
            m_currentHealth -= damage;
            DamageTakenAnimation();
            
            if(m_currentHealth <= 0)
                Die();
        }

        public void SpawnDamageEffect(Vector3 atLocation)
        {
            if (m_damageTakenEffect != null)
            {
                var effect = Instantiate(m_damageTakenEffect);
                effect.transform.position = atLocation;
            }
        }

        #endregion Damageable Methods
    }
}