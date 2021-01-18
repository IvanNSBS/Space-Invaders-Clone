using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleKiller : MonoBehaviour
    {
        #region Fields
        private ParticleSystem m_particleSystem;
        #endregion Fields
        
        #region MonoBehaviour Methods
        private void Awake()
        {
            m_particleSystem = GetComponent<ParticleSystem>();
            Destroy(gameObject, m_particleSystem.main.duration);
        }
        #endregion MonoBehaviour Methods
    }
}