using Managers;
using UnityEngine;

namespace Invaders
{
    #pragma warning disable CS0649
    [RequireComponent(typeof(InvaderHealth))]
    public class InvaderScore : MonoBehaviour
    {
        #region Fields
        [SerializeField] private int m_score;
        #endregion Fields

        
        #region MonoBehaviour Methods
        private void Awake()
        {
            GetComponent<InvaderHealth>().onDeath += AddScore;
        }

        private void OnDestroy()
        {
            GetComponent<InvaderHealth>().onDeath -= AddScore;
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