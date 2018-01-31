using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float m_Speed;

    public float m_MaxSpeed;

    public Rigidbody2D m_Rigidbody;

    public int m_PolishLevel = 0;

    public TrailRenderer m_Trail;

	
    IEnumerator ResetTrailRenderer(TrailRenderer tr)
    {
        float trailTime = tr.time;
        tr.time = 0;
        yield return null;
        tr.time = trailTime;
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

    public void Polish()
    {
        m_PolishLevel++;
        if(m_PolishLevel == 1)
        {
            m_Trail.gameObject.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End"))
        {
            //SWITCH
            if(m_PolishLevel >= 1)
            {
                StartCoroutine(ResetTrailRenderer(m_Trail));
            }
            GameManager.instance.NextLevel(collision.gameObject.GetComponent<LevelEnd>().m_Rating);
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            GameManager.instance.CollectCoin();
        }
    }
}
