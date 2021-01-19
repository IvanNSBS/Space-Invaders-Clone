using Common;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : EntityHealth
    {
        #region EntityHealth Methods
        protected override void Die()
        {
            
        }

        protected override void DamageTakenAnimation()
        {
            transform.DOShakeScale(0.3f, new Vector3(0.8f, 0.9f), 15);
        }
        #endregion EntityHealth Methods
    }
}