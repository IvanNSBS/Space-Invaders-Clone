using DG.Tweening;
using UnityEngine;

namespace Player.Barricade
{
    #pragma warning disable CS0649
    public class Barricade : MonoBehaviour
    {
        #region Inspector Fields
        [Header("Barricade")]
        [SerializeField] private int m_rows;
        [SerializeField] private int m_columns;
        [SerializeField] private GameObject m_barricadePrefab;
        [SerializeField] private LayerMask m_barricadeLayer;

        [Header("View")]
        [SerializeField] private float m_barricadeCelSize;
        [SerializeField] private Color m_barricadeColor;
        [SerializeField] private GameObject m_damagedEffect;
        #endregion Inspector Fields
        
        #region Fields
        #endregion Fields
        
        
        #region MonoBehaviour Methods
        private void Awake()
        {
            SpawnBarricades();
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        private void SpawnBarricades()
        {
            foreach (Transform children in transform)
            {
                DestroyImmediate(children.gameObject);
            }

            float halfSize = m_barricadeCelSize / 2f; 
            for (int i = 0; i < m_rows; i++)
            {
                float xPos = i / 2f * m_barricadeCelSize + halfSize;
                for (int j = 0; j < m_columns; j++)
                {
                    var barricade = Instantiate(m_barricadePrefab, transform);
                    
                    var block = barricade.GetComponent<BarricadeBlock>();
                    block.SetupBarricade(m_barricadeColor, m_damagedEffect, this);
                    
                    float yPos = j / 2f * m_barricadeCelSize + halfSize;
                    Vector3 localPosition = new Vector3(xPos, yPos, 0);
                    barricade.transform.localPosition = localPosition;
                    
                    barricade.layer = (int)Mathf.Log(m_barricadeLayer.value, 2);
                }
            }
        }

        public void PlayDamagedAnimation()
        {
            transform.DOShakeScale(0.35f, new Vector3(0.3f, 0.25f), 17);
        }
        #endregion Methosd
    }
}