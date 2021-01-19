using Common;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Managers;
using Managers.Services;
using DotNetRandom = System.Random;
using UnityRandom = UnityEngine.Random;

namespace Invaders
{
    #pragma warning disable CS0649
    [RequireComponent(typeof(InvadersMovement))]
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
        [SerializeField] private Transform m_yTargetTransform;

        [Header("Invader Shooting")] 
        [Range(0.1f, 5.0f)]
        [SerializeField] private float m_delayBetweenShots;

        [Header("Special Invader")] 
        [SerializeField] private Transform m_leftSpawnPoint;
        [SerializeField] private Transform m_rightSpawnPoint;
        [SerializeField] private GameObject m_specialInvaderPrefab;
        [SerializeField] private float m_timeBetweenSpawnSpecialInvader;
        #endregion Inspector Fields
        
        
        #region Fields
        private List<BulletShooter> m_invaderShooters;
        private float m_currentShotDelay;
        private Vector3 m_initialTransformPosition;
        private InvadersMovement m_invadersMovement;
        private float m_currentSpecialInvaderCooldown;
        #endregion Fields
        
        
        #region Properties
        public List<GameObject> InvaderRows => m_rows;

        public float YPositionTarget => m_yTargetTransform != null ? m_yTargetTransform.position.y : -999999f;
        #endregion Properties
        
        
        #region MonoBehaviour Methods
        private void Awake()
        {
            SpawnInvaders();
            m_initialTransformPosition = transform.position;
            m_currentShotDelay = m_delayBetweenShots;
            m_invadersMovement = GetComponent<InvadersMovement>();
            m_currentSpecialInvaderCooldown = m_timeBetweenSpawnSpecialInvader;
            
            ServiceLocator.Current.GetService<ControllersFinder>().SetInvadersData(this);
        }

        private void Update()
        {
            m_currentShotDelay -= Time.deltaTime;
            m_currentSpecialInvaderCooldown -= Time.deltaTime;
            
            if (m_currentShotDelay <= 0.0f)
            {
                Shoot();
                m_currentShotDelay = m_delayBetweenShots;
            }

            if (m_currentSpecialInvaderCooldown <= 0.0f)
            {
                m_currentSpecialInvaderCooldown = m_timeBetweenSpawnSpecialInvader;
                SpawnSpecialInvader();
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
                    var positionChecker = invader.GetComponent<InvaderCheckPositionLimit>();
                    if (bulletShooter != null)
                    {
                        m_invaderShooters.Add(bulletShooter);
                        if (invaderHealth != null)
                            invaderHealth.Initialize(bulletShooter, this);
                    }
                    if (positionChecker != null && m_yTargetTransform != null)
                    {
                        positionChecker.SetYTarget(m_yTargetTransform.position.y);
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

        private void SpawnSpecialInvader()
        {
            float random = Random.Range(0, 1);
            var specialInvader = Instantiate(m_specialInvaderPrefab);
            
            if (random >= 0.5f)
            {
                specialInvader.transform.position = m_leftSpawnPoint.position;
                specialInvader.GetComponent<SpecialInvaderController>().StartSpecialInvaderController(this, 1);
            }
            else
            {
                specialInvader.transform.position = m_rightSpawnPoint.position;
                specialInvader.GetComponent<SpecialInvaderController>().StartSpecialInvaderController(this, -1);
            }
        }

        public void StopInvaders()
        {
            m_invadersMovement.StopMovement();
            m_currentShotDelay = 99999f;
            m_delayBetweenShots = 9999f;
            m_currentSpecialInvaderCooldown = 99999f;
            m_timeBetweenSpawnSpecialInvader = 99999f;
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
