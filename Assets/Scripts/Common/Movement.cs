using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{   
    #region Unity Fields
    [SerializeField] private float m_movementSpeed;
    #endregion Unity Fields

    #region Fields
    private Rigidbody2D m_rigidbody2D;
    #endregion Fields

    #region Properties
    public float MovementSpeed => m_movementSpeed;
    #endregion
    
    #region MonoBehaviour Methods
    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void Update() {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
    #endregion MonoBehaviour Methods
    
    
    #region Methods
    public void Move(float horizontalDirection)
    {
        m_rigidbody2D.velocity = new Vector3(horizontalDirection * MovementSpeed, 0, 0) * Time.deltaTime;
    }
    #endregion Methods
}
