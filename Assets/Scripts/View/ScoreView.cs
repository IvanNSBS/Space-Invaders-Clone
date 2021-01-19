using TMPro;
using Managers;
using UnityEngine;

namespace View
{
    #pragma warning disable CS0649
    public class ScoreView : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField] private TextMeshProUGUI m_currentScoreText;
        [SerializeField] private TextMeshProUGUI m_hiScoreText;
        #endregion Inspector Fields
        
        #region Fields
        private ScoreManager m_scoreManagerRef;
        #endregion Fields
        
        #region MonoBehaviour Methods
        private void Awake()
        {
            m_scoreManagerRef = ServiceLocator.Current.GetService<ScoreManager>();
            if (m_scoreManagerRef != null)
            {
                m_scoreManagerRef.onAddScore += UpdateScore;
            }
            
            UpdateScore();
        }

        private void OnDestroy()
        {
            m_scoreManagerRef.onAddScore -= UpdateScore;
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        private void UpdateScore()
        {
            if (m_scoreManagerRef == null)
                return;
            
            if(m_hiScoreText != null)
                m_hiScoreText.text = m_scoreManagerRef.HiScore.ToString();

            if (m_currentScoreText != null)
                m_currentScoreText.text = m_scoreManagerRef.CurrentScore.ToString();
        }
        #endregion Methods
    }
}