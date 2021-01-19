using Common;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using DotNetRandom = System.Random;
using UnityRandom = UnityEngine.Random;

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

        [Header("Invader Shooting")] 
        [Range(0.1f, 5.0f)]
        [SerializeField] private float m_delayBetweenShots;
        
        [Header("Special Invader")] 
        [SerializeField] private GameObject m_specialInvaderPrefab;
        [SerializeField] private float m_specialInvaderSpawnDelay;
        #endregion Inspector Fields
        
        
        #region Fields
        private List<BulletShooter> m_invaderShooters;
        private float m_currentShotDelay;
        private Vector3 m_initialTransformPosition;
        #endregion Fields
        
        #region MonoBehaviour Methods
        private void Awake()
        {
            SpawnInvaders();
            m_initialTransformPosition = transform.position;
            m_currentShotDelay = m_delayBetweenShots;
        }

        private void Update()
        {
            m_currentShotDelay -= Time.deltaTime;
            if (m_currentShotDelay <= 0.0f)
            {
                Shoot();
                m_currentShotDelay = m_delayBetweenShots;
            }
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        private void SpawnInvaders()
        {
            float halfCellSize = m_invaderCellSize / 2f;
            float xOffset = -(m_numberOfInvadersInRow / 2f) * m_invaderCellSize + halfCellSize;
            
            float currentYOffset = 0;
            float currentXOffset = xOffset;
            float outsideScreenY = GetOutsideScreenYPos();

            m_invaderShooters = new List<BulletShooter>();
            
            foreach (var invaderPrefab in m_rows)
            {
                for (int i = 0; i < m_numberOfInvadersInRow; i++)
                {
                    if(invaderPrefab == null)
                        continue;
                    
                    var invader = Instantiate(invaderPrefab, transform);
                    var bulletShooter = invader.GetComponent<BulletShooter>();
                    var invaderHealth = invader.GetComponent<InvaderHealth>();
                    if (bulletShooter != null)
                    {
                        m_invaderShooters.Add(bulletShooter);
                        if (invaderHealth != null)
                            invaderHealth.Initialize(bulletShooter, this);
                    }
                    
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
            sequence.AppendInterval(UnityRandom.Range(m_spawnDelayRange.x, m_spawnDelayRange.y));
            sequence.Append(invaderTransform.DOMoveY(targetYPosition, m_spawnMoveDuration).SetEase(Ease.OutBack, m_overShoot));
        }

        private void Shoot()
        {
            if (m_invaderShooters.Count == 0)
                return;
            
            var random = new DotNetRandom();
            int index = random.Next(m_invaderShooters.Count);
            m_invaderShooters[index].Shoot();
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

            transform.position = m_initialTransformPosition;
            SpawnInvaders();
        }

        public float GetLateralHerdSize()
        {            
            float halfCellSize = m_invaderCellSize / 2f;
            float xOffset = (m_numberOfInvadersInRow / 2f) * m_invaderCellSize + halfCellSize;
            return xOffset;
        }

        public bool RemoveShooter(BulletShooter shooter)
        {
            bool result = m_invaderShooters.Remove(shooter);
            if (m_invaderShooters.Count == 0)
            {
                Sequence sequence = DOTween.Sequence();
                sequence.AppendInterval(0.7f);
                sequence.AppendCallback(RespawnInvaders);
            }
            
            return result;
        }
        #endregion Utility Methods
    }
}
