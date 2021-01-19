using Managers;
using UnityEngine;

namespace Invaders
{
    [RequireComponent(typeof(InvaderHealth))]
    public class InvaderScore : MonoBehaviour
    {
        #region Fields
        [SerializeField] private int m_score;
        #endregion Fields

        
        #region MonoBehaviour Methods
        private void Awake()
        {
            GetComponent<InvaderHealth>().onInvaderDeath += AddScore;
        }

        private void OnDestroy()
        {
            GetComponent<InvaderHealth>().onInvaderDeath -= AddScore;
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        private void AddScore()
        {
            ServiceLocator.Current.GetService<ScoreManager>()?.AddScore(m_score);       
        }
        #endregion Methods
    }
}