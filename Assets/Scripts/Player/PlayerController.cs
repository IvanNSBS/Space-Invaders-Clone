using Common;
using UnityEngine;
using Player.Input;
using UnityEngine.InputSystem;

namespace Player
{
    #pragma warning disable CS0649
    [RequireComponent(typeof(Movement), typeof(BulletShooter))]
    public class PlayerController : MonoBehaviour
    {
        #region Unity Fields
        [SerializeField] private float m_timeBetweenShots = 1.5f;
        #endregion Unity Fields
        
        #region Fields
        private Movement m_movement;
        private BulletShooter m_bulletShooter;
        private PlayerInputHandler m_playerInput;
        private float m_currentCooldown;
        private bool m_shooting;
        #endregion Fields
        
        
        #region MonoBehaviour Methods
        private void Awake()
        {
            m_playerInput = new PlayerInputHandler();
            m_playerInput.Actions.Move.performed += PerformMovement;
            m_playerInput.Actions.Move.canceled += StopMovement;
            m_playerInput.Actions.Shoot.performed += StartShooting;
            m_playerInput.Actions.Shoot.canceled += StopShooting;

            m_movement = GetComponent<Movement>();
            m_bulletShooter = GetComponent<BulletShooter>();
        }

        private void Update()
        {
            m_currentCooldown -= Time.deltaTime;
            m_currentCooldown = Mathf.Clamp(m_currentCooldown, 0, m_timeBetweenShots);
            
            if (m_currentCooldown <= 0.001f && m_shooting)
            {
                m_bulletShooter.Shoot();
                m_currentCooldown = m_timeBetweenShots;
            }
        }

        private void OnEnable()
        {
            m_playerInput.Enable();
            m_playerInput.Actions.Move.performed += PerformMovement;
            m_playerInput.Actions.Move.canceled += StopMovement;
            m_playerInput.Actions.Shoot.performed += StartShooting;
            m_playerInput.Actions.Shoot.canceled += StopShooting;
        }

        private void OnDisable()
        {
            m_playerInput.Disable();
            m_playerInput.Actions.Move.performed -= PerformMovement;
            m_playerInput.Actions.Move.canceled -= StopMovement;
            m_playerInput.Actions.Shoot.canceled -= StopShooting;
            m_playerInput.Actions.Shoot.canceled -= StopShooting;
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        private void PerformMovement(InputAction.CallbackContext context)
        {
            m_movement.Move(context.ReadValue<float>());
        }

        private void StopMovement(InputAction.CallbackContext context)
        {
            m_movement.Move(0);
        }

        private void StartShooting(InputAction.CallbackContext context)
        {
            m_shooting = true;
        }
        
        private void StopShooting(InputAction.CallbackContext context)
        {
            m_shooting = false;
        }
        #endregion Methods
    }
}
