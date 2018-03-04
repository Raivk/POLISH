using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {

    public Transform m_Player;

    public float m_XDistance;
    public float m_YDistance;

    public float m_Lerper;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        MoveCamera();
	}

    void MoveCamera()
    {
        float dist_x = Mathf.Abs(m_Player.position.x - transform.position.x);
        float dist_y = Mathf.Abs(m_Player.position.y - transform.position.y);
        float lerperX = m_Lerper;
        float lerperY = m_Lerper;

        Vector2 nextPosition = transform.position;

        if (dist_x >= m_XDistance)
        {
            lerperX = (dist_x / m_XDistance) * Time.deltaTime;
            nextPosition.x = m_Player.position.x;
        }
        if (dist_y >= m_YDistance)
        {
            lerperY = (dist_y / m_YDistance) * Time.deltaTime;
            nextPosition.y = m_Player.position.y;
        }

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, nextPosition.x, lerperX), Mathf.Lerp(transform.position.y, nextPosition.y, lerperY), -10);
    }
}
