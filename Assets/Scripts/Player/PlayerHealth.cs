using Common;
using DG.Tweening;
using Managers;
using Managers.Services;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : EntityHealth
    {
        #region Fields
        private bool m_dying;
        #endregion Fields
        
        #region EntityHealth Methods
        protected override void Die()
        {
            if (m_dying)
                return;

            m_dying = true;
            
            var invadersController = ServiceLocator.Current.GetService<ControllersFinder>().InvadersController;
            invadersController.StopInvaders();
            GameSessionManager.Instance.EndSession(2f);
            
            SpawnDamageEffect(gameObject.transform.position);
            
            Destroy(gameObject);
        }

        protected override void DamageTakenAnimation()
        {
            transform.DOShakeScale(0.3f, new Vector3(0.8f, 0.9f), 15);
        }
        #endregion EntityHealth Methods
    }
}