using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour {

    public float FireInterval = 1f;
    private float lastFired = 0;

    public Transform m_Canon;

	// Use this for initialization
	void Start () {
        lastFired = 0;
	}
	
	// Update is called once per frame
	void Update () {
        lastFired += Time.deltaTime;

		if(lastFired >= FireInterval)
        {
            lastFired = 0;
            Fire();
        }
	}

    public void Fire()
    {
        GameObject laser = ObjectPooler.SharedInstance.GetPooledObject(0);
        laser.transform.position = m_Canon.position;
        laser.transform.rotation = m_Canon.rotation;

        laser.GetComponent<LaserController>().ResetLaser();
        laser.SetActive(true);
    }
}
