using Common;
using DG.Tweening;
using UnityEngine;

namespace Invaders
{
    public class InvaderHealth : EntityHealth
    {
        #region Inspector Fields
        [SerializeField] private GameObject m_deathParticle;
        #endregion Inspector Fields
        
        #region Fields
        private Sequence m_damageSequence;
        #endregion Fields
        
        #region EntityHealth Methods
        protected override void Die()
        {
            m_damageSequence.Kill();
            
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(new Vector3(0.75f, 0.75f, 1), 0.15f));
            sequence.Append(transform.DOScale(new Vector3(1.9f, 1.9f, 1), 0.5f).SetEase(Ease.OutQuint));
            sequence.AppendCallback(() =>
            {
                if (m_deathParticle != null)
                {
                    var deathParticle = Instantiate(m_deathParticle);
                    deathParticle.transform.position = gameObject.transform.position;
                }
                Destroy(gameObject);
            });
        }

        protected override void DamageTakenAnimation()
        {
            m_damageSequence = DOTween.Sequence();
            m_damageSequence.Append(transform.DOShakeScale(0.4f, new Vector3(0.8f, 0.9f), 15));
        }
        #endregion EntityHealth Methods
    }
}