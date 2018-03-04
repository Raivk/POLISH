using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

    public float m_Speed;

    public float m_MaxSpeed;

    public Rigidbody2D m_Rigidbody;

    public UnityEvent m_OnTeleport;

    private void FixedUpdate()
    {
        //MOVE
        HandleMovement();

        //CLAMP
        m_Rigidbody.velocity = Vector3.ClampMagnitude(m_Rigidbody.velocity, m_MaxSpeed);
    }

    public void HandleMovement()
    {
        Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        m_Rigidbody.AddForce(inputs * m_Speed, ForceMode2D.Impulse);
    }

    public void Respawn(Level current)
    {
        m_OnTeleport.Invoke();
        transform.position = new Vector3(current.m_Start.position.x, current.m_Start.position.y, transform.position.z);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End"))
        {
            //SWITCH
            GameManager.instance.NextLevel(collision.gameObject.GetComponent<LevelEnd>().m_Rating);
            m_OnTeleport.Invoke();
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            GameManager.instance.CollectCoin();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            GameManager.instance.Die();
        }
    }
}
