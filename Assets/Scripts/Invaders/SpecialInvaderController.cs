using Common;
using DG.Tweening;
using UnityEngine;
using DotNetRandom = System.Random;

namespace Invaders
{
    #pragma warning disable CS0649
    [RequireComponent(typeof(Movement))]
    public class SpecialInvaderController : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField] private float m_timeBetweenSpawnInvaders = 0.2f;
        [SerializeField] [Range(0, 5)] private int m_maxInvaderSpawn = 1;
        #endregion Inspector Fields
        
        #region Fields
        private InvadersController m_invadersController;
        private float m_currentSpawnCooldown;
        private Movement m_movement;
        private float m_moveDirection = 1.0f;
        #endregion Fields
        
        #region Properties
        private float Duration => m_timeBetweenSpawnInvaders * m_maxInvaderSpawn + 0.5f;
        #endregion Properties
        

        #region MonoBehaviour Methods
        private void Awake()
        {
            m_currentSpawnCooldown = m_timeBetweenSpawnInvaders;
            m_movement = GetComponent<Movement>();        
            
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(Duration);
            sequence.Append(transform.DOScale(new Vector3(0.3f, 0.3f), 0.5f).SetEase(Ease.InOutBack));
            sequence.AppendCallback(() =>
            {
                Destroy(gameObject);
            });
        }

        private void Update()
        {
            m_movement.Move(m_moveDirection);
            
            m_currentSpawnCooldown -= Time.deltaTime;
            if (m_currentSpawnCooldown <= 0.0f)
            {
                SpawnInvader();
                m_currentSpawnCooldown = m_timeBetweenSpawnInvaders;
            }
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        public void StartSpecialInvaderController(InvadersController controller, float moveDirection)
        {
            m_moveDirection = moveDirection;
            m_invadersController = controller;
        }

        private void SpawnInvader()
        {
            var random = new DotNetRandom();
            int index = random.Next(m_invadersController.InvaderRows.Count);
            
            var invader = Instantiate(m_invadersController.InvaderRows[index], m_invadersController.transform);
            invader.transform.position = transform.position + new Vector3(0, 0.1f, 0f);
            invader.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            
            invader.transform.DOMoveY(invader.transform.position.y - 0.5f, 0.2f).SetEase(Ease.OutBack);
            invader.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.2f).SetEase(Ease.OutQuart);   
            
            invader.GetComponent<InvaderCheckPositionLimit>().SetYTarget(m_invadersController.YPositionTarget);
        }
        #endregion Methods
    }
}