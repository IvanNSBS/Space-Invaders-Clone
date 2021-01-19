using DG.Tweening;
using UnityEngine;

namespace Invaders
{
    [RequireComponent(typeof(InvadersController))]
    #pragma warning disable CS0649
    public class InvadersMovement : MonoBehaviour
    {
        #region Inspector Fields
        [Header("Horizontal Movement")]
        [SerializeField] private float m_horizontalSpeed;
        [SerializeField] private float m_startSideMovementDelay;
        
        [Header("Vertical Movement")]
        [SerializeField] private float m_verticalMovement = 0.5f;
        [SerializeField] private float m_moveDownSpeed = 1f;
        #endregion Inspector Fields
        
        #region Fields
        private float m_moveDirection;
        private InvadersController m_invadersController;
        private bool m_movingDown;
        #endregion Fields
        
        
        #region MonoBehaviour Methods
        private void Awake()
        {
            m_invadersController = GetComponent<InvadersController>();

            m_moveDirection = 0f;
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(m_startSideMovementDelay);
            sequence.AppendCallback(() => m_moveDirection = 1f);
        }

        private void Update()
        {
            if (m_movingDown)
                return;
            
            float sideSize = m_invadersController.GetLateralHerdSize();
            
            Vector3 pos = transform.position;
            if (m_moveDirection >= 0.99f) //moving right
            {
                pos = Camera.main.WorldToViewportPoint(transform.position + new Vector3(sideSize, 0f, 0f));
                //reached right side
                if (pos.x >= 0.99f)
                {
                    AnimateStepDown(-1f);
                }
            }
            else
            {
                pos = Camera.main.WorldToViewportPoint(transform.position + new Vector3(-sideSize, 0f, 0f));
                //reached left side
                if (pos.x <= 0.01f)
                {
                    AnimateStepDown(1f);
                }
            }

            transform.position += new Vector3(m_moveDirection * m_horizontalSpeed, 0)*Time.deltaTime;
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        private void AnimateStepDown(float nextMovementDirection)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                m_moveDirection = 0f;
                m_movingDown = true;
            });
            sequence.Append(transform.DOMoveY(transform.position.y - m_verticalMovement, m_moveDownSpeed));
            sequence.AppendCallback(() =>
            {
                m_moveDirection = nextMovementDirection;
                m_movingDown = false;
            });
        }
        #endregion Methods
    }
}
