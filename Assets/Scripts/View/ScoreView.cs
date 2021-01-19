using DG.Tweening;
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
        private int m_currentHighScore;
        private Sequence m_animateHiScore;
        private Sequence m_animateScore;
        #endregion Fields
        
        #region MonoBehaviour Methods
        private void Start()
        {
            m_scoreManagerRef = ServiceLocator.Current.GetService<ScoreManager>();
            m_currentHighScore = m_scoreManagerRef.HiScore;
            
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

            if (m_hiScoreText != null)
            {
                m_hiScoreText.text = m_scoreManagerRef.HiScore.ToString();
 
                int newHiScore = m_scoreManagerRef.HiScore;
                if (newHiScore > m_currentHighScore)
                {
                    m_currentHighScore = newHiScore;
                    m_animateHiScore.Kill();
                    m_hiScoreText.transform.localScale = Vector3.one;
                    m_hiScoreText.color = Color.white;
                    
                    m_animateHiScore = DOTween.Sequence();
                    m_animateHiScore.Append(m_hiScoreText.DOColor(Color.green, 0.2f));
                    m_animateHiScore.Append(m_hiScoreText.DOColor(Color.white, 0.1f));
                    m_animateHiScore.Insert(0f, m_hiScoreText.transform.DOShakeScale(0.3f, new Vector3(0.9f, 1f), 16));
                }
            }

            if (m_currentScoreText != null)
            {
                m_animateScore.Kill();                
                m_currentScoreText.text = m_scoreManagerRef.CurrentScore.ToString();
                m_currentScoreText.transform.localScale = Vector3.one;

                m_animateScore = DOTween.Sequence();
                m_animateScore.Append(m_currentScoreText.transform.DOShakeScale(0.25f, new Vector3(0.6f, 0.65f), 14));
            }
        }
        #endregion Methods
    }
}