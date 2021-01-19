using DG.Tweening;
using UnityEngine;
using Common.Interfaces;

namespace Common
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(SpriteRenderer))]
    public class Bullet : MonoBehaviour
    {
        #region Fields
        private Rigidbody2D m_rigidbody2D;
        private SpriteRenderer m_spriteRenderer;
        private int m_bulletDamage;
        private Sequence m_destroyTimer;
        #endregion Fields
        
        
        #region MonoBehaviour Methods
        private void Awake()
        {
            m_rigidbody2D = GetComponent<Rigidbody2D>();
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            m_destroyTimer = DOTween.Sequence();

            // Destroy bullet after some time if it didn't hit anything
            m_destroyTimer.AppendInterval(6.0f);
            m_destroyTimer.AppendCallback(() => Destroy(gameObject));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable == null)
                return;
            
            damageable.SpawnDamageEffect(gameObject.transform.position);
            damageable.TakeDamage(m_bulletDamage);    
            
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            m_destroyTimer.Kill();
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        public void StartBullet(
            LayerMask layerMask, 
            float bulletSpeed, 
            int bulletDamage, 
            float verticalDirection, 
            Color spawnColor)
        {
            gameObject.layer = (int)Mathf.Log(layerMask.value, 2);
            m_rigidbody2D.velocity = new Vector2(0, bulletSpeed * verticalDirection)*Time.deltaTime;
            m_bulletDamage = bulletDamage;
            m_spriteRenderer.color = spawnColor;
        }
        #endregion Methods
    }
}