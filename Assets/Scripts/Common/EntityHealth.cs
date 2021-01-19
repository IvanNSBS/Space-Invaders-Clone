using System;
using UnityEngine;
using Common.Interfaces;
using Managers;

namespace Common
{
    [RequireComponent(typeof(BoxCollider2D))]
    #pragma warning disable CS0649
    public abstract class EntityHealth : MonoBehaviour, IDamageable
    {
        #region Inspector Fields
        [Header("Health")]
        [SerializeField] private int m_maxHealth;
        [SerializeField] private GameObject m_damageTakenEffect;
        
        [Header("Audio")] 
        [SerializeField] private AudioClip m_damageTakenSound;
        [SerializeField] private AudioClip m_deathSound;
        [SerializeField] [Range(0, 1)] private float m_soundVolume = 1;
        #endregion Inspector Fields

        #region Fields
        private int m_currentHealth;
        #endregion Fields
        
        
        #region Properties
        public int MaxHealth => m_maxHealth;
        #endregion Properties
        

        #region Public Fields
        public Action onDamageTaken;
        public Action onDeath;
        #endregion Public Fields
        
        
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
            onDamageTaken?.Invoke();
            
            if (m_damageTakenSound != null)
            {
                ServiceLocator.Current.GetService<SoundManager>().
                    PlayAudioClipAtLocation(m_damageTakenSound, m_soundVolume, transform.position);
            }

            if (m_currentHealth <= 0)
            {
                if (m_deathSound != null)
                {
                    ServiceLocator.Current.GetService<SoundManager>().
                        PlayAudioClipAtLocation(m_deathSound, m_soundVolume, transform.position);
                }
                
                Die();
            }
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