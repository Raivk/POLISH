using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolishController : MonoBehaviour {

    public int m_PolishLevel;
    public int m_NotationType;

    public bool isEnabled = false;
	
	// Update is called once per frame
	void Update () {
        GameManager man = GameManager.instance;
        if(man != null)
        {
            if (isEnabled)
            {
                OnEnabledUpdate();
            } else
            {
                if (man.m_Notations.Count > m_PolishLevel - man.m_MinimumPolishLevel)
                {
                    if (man.m_Notations[m_PolishLevel - man.m_MinimumPolishLevel] == m_NotationType)
                    {
                        OnEnabledUpdate();
                    }
                }
            }
        }
	}

    public virtual void OnEnabledUpdate()
    {
        isEnabled = true;
    }
}
