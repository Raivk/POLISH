using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private int currentLevel = 0;
    public Level[] m_Levels;

    public Level m_RatingRoom;

    private bool m_Rating = false;

    public static GameManager instance;

    public PlayerController m_PlayerController;

    public int m_MinimumPolishLevel;

    public void Awake()
    {
        if (instance == null)
            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextLevel(int rating)
    {
        if (m_Rating)
        {
            currentLevel++;

            if (currentLevel >= m_Levels.Length)
                return;

            if(currentLevel >= m_MinimumPolishLevel)
            {
                m_PlayerController.m_PolishLevel++;
            }

            Level current = m_Levels[currentLevel];

            current.gameObject.SetActive(true);

            m_PlayerController.transform.position = new Vector3(current.m_Start.position.x, current.m_Start.position.y, m_PlayerController.transform.position.z);
            
            string txt = "";
            switch (rating)
            {
                case 0:
                    txt = "0 ?! That's REALLY harsh !";
                    break;
                case 1:
                    txt = "Well... it's... a rating.";
                    break;
                case 2:
                    txt = "2... At least it's honest !";
                    break;
                case 3:
                    txt = "3 is almots 5 !";
                    break;
                case 4:
                    txt = "So close to 5. SO CLOSE.";
                    break;
                case 5:
                    txt = "WOOT ! It's almost good, isn't it ?";
                    break;
                case 6:
                    txt = "I knew it would please you.";
                    break;
                case 7:
                    txt = "7 ! You're so kind";
                    break;
                case 8:
                    txt = "I know I know, this game is amazing.";
                    break;
                case 9:
                    txt = "Almost a perfect game.";
                    break;
                case 10:
                    txt = "10 OUT OF 10 MEANS ABSOLUTELY PERFECT !";
                    break;
            }
            current.Write(txt, current.m_ReactText, true);
        }
        else
        {
            m_Levels[currentLevel].gameObject.SetActive(false);

            m_PlayerController.transform.position = new Vector3(m_RatingRoom.m_Start.position.x, m_RatingRoom.m_Start.position.y, m_PlayerController.transform.position.z);

            m_RatingRoom.Write(m_RatingRoom.m_Text.text, m_RatingRoom.m_Text, false);
        }

        m_Rating = !m_Rating;
    }
}
