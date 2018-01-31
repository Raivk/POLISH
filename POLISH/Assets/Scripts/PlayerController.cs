using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float m_Speed;

    public float m_MaxSpeed;

    public Rigidbody2D m_Rigidbody;

    public int m_PolishLevel = 0;

	// Use this for initialization
	void Start () {
		
	}

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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End"))
        {
            //SWITCH
            GameManager.instance.NextLevel(collision.gameObject.GetComponent<LevelEnd>().m_Rating);
        }
    }
}
