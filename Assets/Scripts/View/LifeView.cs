using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Managers.Services;
using System.Collections.Generic;
using DG.Tweening;
using Player;

namespace View
{
    #pragma warning disable CS0649
    public class LifeView : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField] private Sprite m_heartSprite;
        #endregion Inspector Fields
        
        #region Fields
        public List<GameObject> m_heartImages;
        #endregion Fields

        
        #region MonoBehaviour Methods
        private void Awake()
        {
            m_heartImages = new List<GameObject>();
        }

        private void Start()
        {
            PlayerController playerController = ServiceLocator.Current.GetService<ControllersFinder>().PlayerController;
            playerController.PlayerHealth.onDamageTaken += PlayerDamaged;
            
            for (int i = 0; i < playerController.PlayerHealth.MaxHealth; i++)
            {
                GameObject heart = new GameObject($"Heart {i}");
                heart.transform.parent = transform;

                Image heartImage = heart.AddComponent<Image>();
                heartImage.sprite = m_heartSprite;
                heartImage.color = Color.green;

                RectTransform rectTransform = heart.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(35, 35);
                
                m_heartImages.Add(heart);
            }
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        private void PlayerDamaged()
        {
            if (m_heartImages.Count == 0)
                return;
            
            Transform heartTransform = m_heartImages[m_heartImages.Count - 1].transform;
            
            Sequence sequence = DOTween.Sequence();
            sequence.Append(heartTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.45f).SetEase(Ease.InQuint));
            sequence.Append(heartTransform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.3f).SetEase(Ease.InBack));
            sequence.AppendCallback(() =>
            {
                m_heartImages.RemoveAt(m_heartImages.Count-1);
                Destroy(heartTransform.gameObject);
            });
        }
        #endregion Methods
    }
}