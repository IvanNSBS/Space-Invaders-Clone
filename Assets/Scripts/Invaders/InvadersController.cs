using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

namespace Invaders
{
    #pragma warning disable CS0649
    public class InvadersController : MonoBehaviour
    {
        #region Inspector Fields
        [Header("Spawn Animation")] 
        [SerializeField] private float m_spawnMoveDuration = 1.5f;
        [SerializeField] private Vector2 m_spawnDelayRange = new Vector2(0.1f, 0.5f);
        [SerializeField] private float m_overShoot = 0.5f;
        
        [Header("Invaders")]
        [SerializeField] private int m_numberOfInvadersInRow = 11;
        [SerializeField] private List<GameObject> m_rows;
        [SerializeField] private float m_invaderCellSize = 5f;

        [Header("Special Invader")] 
        [SerializeField] private GameObject m_specialInvaderPrefab;
        [SerializeField] private float m_specialInvaderSpawnDelay;
        #endregion Inspector Fields
        
        
        #region MonoBehaviour Methods
        private void Awake()
        {
            SpawnInvaders();
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        private void SpawnInvaders()
        {
            int rowCount = m_rows.Count;
            float halfCellSize = m_invaderCellSize / 2f;
            float xOffset = -(m_numberOfInvadersInRow / 2f) * m_invaderCellSize + halfCellSize;
            
            float currentYOffset = 0;
            float currentXOffset = xOffset;
            float outsideScreenY = GetOutsideScreenYPos();
            
            foreach (var invaderPrefab in m_rows)
            {
                for (int i = 0; i < m_numberOfInvadersInRow; i++)
                {
                    if(invaderPrefab == null)
                        continue;
                    
                    var invader = Instantiate(invaderPrefab, transform);
                    Vector3 spawnPosition = transform.position + new Vector3(currentXOffset, outsideScreenY, 0);
                    invader.transform.position = spawnPosition;
                    
                    AnimateInvaderSpawn(invader.transform, transform.position.y + currentYOffset);
                    currentXOffset += m_invaderCellSize;
                }

                currentXOffset = xOffset;
                currentYOffset += m_invaderCellSize;
            }
        }
        
        private float GetOutsideScreenYPos()
        {
            Vector3 outsidePosNormalized = new Vector3(0f, 1.001F, 0f);
            Vector3 worldPos = Camera.main.ViewportToWorldPoint(outsidePosNormalized);
            return worldPos.y;
        }

        private void AnimateInvaderSpawn(Transform invaderTransform, float targetYPosition)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(Random.Range(m_spawnDelayRange.x, m_spawnDelayRange.y));
            sequence.Append(invaderTransform.DOMoveY(targetYPosition, m_spawnMoveDuration).SetEase(Ease.OutBack, m_overShoot));
        }
        #endregion Methods
        
        
        #region Utility Methods
        [ContextMenu("Respawn Invaders")]
        private void RespawnInvaders()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            
            SpawnInvaders();
        }

        public float GetLateralHerdSize()
        {            
            float halfCellSize = m_invaderCellSize / 2f;
            float xOffset = (m_numberOfInvadersInRow / 2f) * m_invaderCellSize + halfCellSize;
            return xOffset;
        }
        #endregion Utility Methods
    }
}
