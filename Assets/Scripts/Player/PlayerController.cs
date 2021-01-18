using System;
using UnityEngine;

[RequireComponent(typeof(Movement), typeof(Bullet))]
public class PlayerController : MonoBehaviour
{
    #region Fields
    private Movement m_movement;
    private Bullet m_bullet;
    #endregion Fields
    
    
    #region MonoBehaviour Methods
    private void Awake()
    {
        m_movement = GetComponent<Movement>();
        m_bullet = GetComponent<Bullet>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_movement.Move(-1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_movement.Move(1);
        }
    }

    #endregion MonoBehaviour Methods
    
    
    #region Methods
    #endregion Methods
}
