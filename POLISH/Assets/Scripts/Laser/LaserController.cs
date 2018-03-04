using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

    private GameObject[] m_Childs;

    public float m_MoveSpeed;

    private int aliveChilds;

    private float distance = 0;
    private float childSize = 0.1f;
    private bool full = false;

    private float lastFired = 0;
    public float m_Lifespan = 7.5f;

    private void Awake()
    {
        m_Childs = new GameObject[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            m_Childs[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Start()
    {
        ResetLaser();
    }

    public void ResetLaser()
    {
        lastFired = 0f;
        aliveChilds = 0;
        distance = 0;
        full = false;
        foreach (var child in m_Childs)
        {
            child.SetActive(false);
        }
    }

    public void KillOneLaserObject()
    {
        aliveChilds--;
        if (aliveChilds <= 0 && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        lastFired += Time.deltaTime;
        if(lastFired >= m_Lifespan)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 move = transform.up * m_MoveSpeed * Time.deltaTime;
        distance += Vector2.Distance(transform.position, transform.position + move);

        if (!full)
        {
            for(int i = 0; i < distance / childSize; i++)
            {
                if (m_Childs.Length <= i)
                    break;

                aliveChilds = i;
                m_Childs[i].SetActive(true);
            }

            if(distance / childSize >= m_Childs.Length)
            {
                full = true;
            }
        }

        //MOVE
        transform.position += move;
    }
}
