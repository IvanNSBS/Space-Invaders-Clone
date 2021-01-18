﻿using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setup : MonoBehaviour
{
    #region Unity Fields
    [SerializeField] private Button m_playButton;
    [SerializeField] private float m_buttonAnimationMaxMove = 10f;
    [SerializeField] private float m_moveDuration = 1f;
    #endregion Unity Fields
    
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
            float yPosition = transform.localPosition.y;
            Vector3 finalMoveDown = new Vector3(0, yPosition-m_buttonAnimationMaxMove, 0);
            moveButtonSequence.Append(m_playButton.transform.DOLocalMove(finalMoveDown, m_moveDuration));
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
