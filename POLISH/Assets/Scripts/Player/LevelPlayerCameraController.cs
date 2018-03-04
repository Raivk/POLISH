using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlayerCameraController : MonoBehaviour {

    public Transform m_Player;

    private Vector3 currentLevel;

    private bool switchingLevel = false;

    public float m_LevelSwitchingTime;
    public Ease m_LevelSwitchingAnim;

    public float m_PositionLerper;

	// Use this for initialization
	void Start () {
        SwitchLevel(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (!switchingLevel)
        {
            Vector3 newPos = Vector3.Lerp(currentLevel, new Vector3(m_Player.position.x, m_Player.position.y, currentLevel.z), m_PositionLerper);
            transform.position = Vector3.Lerp(transform.position, newPos, 0.1f);
        }
	}

    public void SwitchLevel(bool anim = true)
    {
        GameManager man = GameManager.instance;

        Transform newPos = null;

        if (man.m_Rating)
        {
            newPos = man.m_RatingRoom.transform;
        } else
        {
            newPos = man.m_Levels[man.currentLevel].transform;
        }

        currentLevel = new Vector3(newPos.position.x, newPos.position.y, transform.position.z);

        //MOVE ANIM
        if (anim)
        {
            switchingLevel = true;

            transform.DOMove(currentLevel, m_LevelSwitchingTime).SetEase(m_LevelSwitchingAnim).OnComplete(delegate
            {
                switchingLevel = false;
            });
        }
    }
}
