using Managers;
using UnityEngine;

namespace Invaders
{
    public class InvaderScore : MonoBehaviour
    {
        #region Fields
        [SerializeField] private int m_score;
        #endregion Fields
        
        
        #region MonoBehaviour Methods
        private void OnDestroy()
        {
            ServiceLocator.Current.GetService<ScoreManager>()?.AddScore(m_score);       
        }
        #endregion MonoBehaviour Methods
    }
}