using Managers;
using UnityEngine;
using Common.Interfaces;

namespace Player.Barricade
{
    #pragma warning disable CS0649
    [RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
    public class BarricadeBlock : MonoBehaviour, IDamageable
    {
        #region Inspector Fields
        [SerializeField] private AudioClip m_hitAudio;
        [SerializeField][Range(0,1)] private float m_soundVolume = 0.065f;
        #endregion Inspector Fields
        
        
        #region Fields
        private GameObject m_damagedEffect;
        private SpriteRenderer m_spriteRenderer;
        private Barricade m_barricade;
        #endregion Fields
        
        
        #region MonoBehaviour Methods
        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        public void SetupBarricade(Color color, GameObject damagedEffect, Barricade barricade)
        {
            m_spriteRenderer.color = color;
            m_damagedEffect = damagedEffect;
            m_barricade = barricade;
        }
        #endregion Methods
        
        
        #region IDamageable Methods
        public void TakeDamage(int damage)
        {
            m_barricade.PlayDamagedAnimation();
            
            if (m_hitAudio != null)
            {
                ServiceLocator.Current.GetService<SoundManager>().
                    PlayAudioClipAtLocation(m_hitAudio, m_soundVolume, transform.position);
            }
            
            Destroy(gameObject);
        }

        public void SpawnDamageEffect(Vector3 atLocation)
        {
            if (m_damagedEffect != null)
            {
                var effect = Instantiate(m_damagedEffect);
                effect.transform.position = atLocation;
            }
        }
        #endregion IDamageable Methods

    }
}