using Common.Interfaces;
using Managers;
using UnityEngine;
using Managers.Services;

namespace Invaders
{
    public class InvaderCheckPositionLimit : MonoBehaviour
    {
        #region Fields
        private float m_yPosTarget = 99999f;
        #endregion Fields
        
        #region Methods
        public void SetYTarget(float yTarget)
        {
            m_yPosTarget = yTarget;
        }
        #endregion Methods
        
        
        #region MonoBehaviour Methods
        private void Update()
        {
            if (transform.position.y <= m_yPosTarget)
            {
                var player = ServiceLocator.Current.GetService<ControllersFinder>().PlayerController;
                if(player != null)
                    player.GetComponent<IDamageable>().TakeDamage(100);
            }
        }
        #endregion MonoBehaviour Methods
    }
}