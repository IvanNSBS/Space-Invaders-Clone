using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace View
{
    public class MainMenu : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField] private Button m_playButton;
        [SerializeField] private float m_moveDuration = 1f;
        #endregion Inspector Fields
        
        #region Fields
        private TextMeshProUGUI m_playText;
        private Sequence moveButtonSequence;
        #endregion Fields
        
        #region MonoBehaviour Methods
        private void Awake()
        {
            m_playButton?.onClick.AddListener(StartGame);
            m_playText = m_playButton?.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            if (m_playButton)
            {
                moveButtonSequence = DOTween.Sequence();
                Vector3 initialScale = new Vector3(0.95f, 0.95f);
                Vector3 finalScale = new Vector3(1.05f, 1.05f);
                
                m_playButton.gameObject.transform.localScale = initialScale;
                moveButtonSequence.Append(m_playButton.transform.DOScale(finalScale, m_moveDuration));
                moveButtonSequence.SetLoops(-1, LoopType.Yoyo);
            }
        }

        #endregion MonoBehaviour Methods
        
        #region Methods
        private void StartGame()
        {
            moveButtonSequence.Kill();
            Sequence sequence = DOTween.Sequence();
            sequence.Append(m_playText.DOColor(new Color(1, 0.7f, 0.2f, 1f), 1.2f).SetEase(Ease.Flash, 18, 0.5f));
            sequence.AppendCallback(() =>
            {
                SceneManager.LoadSceneAsync("Game");
            });
        }
        #endregion Methods
    }
}
