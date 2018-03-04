using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    public int currentLevel = 0;
    public Level[] m_Levels;

    public Level m_RatingRoom;

    [HideInInspector]
    public bool m_Rating = false;

    public static GameManager instance;

    public PlayerController m_PlayerController;

    public int m_MinimumPolishLevel;

    public List<int> m_Notations = new List<int>();

    public UnityEvent OnLevelChange;

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

    public void Start()
    {
        foreach(Level l in m_Levels)
        {
            l.gameObject.SetActive(false);
        }
        m_Levels[0].gameObject.SetActive(true);
    }

    public void CollectCoin()
    {
        if (m_Rating)
            return;

        m_Levels[currentLevel].CollectCoin();
    }

    public void NextLevel(int rating)
    {
        m_Levels[currentLevel].gameObject.SetActive(false);
        if(currentLevel < m_MinimumPolishLevel)
        {
            currentLevel++;

            if (currentLevel >= m_Levels.Length)
                return;

            Level current = m_Levels[currentLevel];

            current.gameObject.SetActive(true);

            m_PlayerController.transform.position = new Vector3(current.m_Start.position.x, current.m_Start.position.y, m_PlayerController.transform.position.z);

            current.Write(current.m_Text.text, current.m_Text, false);
        } else
        {
            if (m_Rating)
            {
                currentLevel++;

                if (currentLevel >= m_Levels.Length)
                    return;

                //Add polish level
                if (currentLevel >= m_MinimumPolishLevel)
                {
                    m_Notations.Add(rating <= 3 ? 1 : (rating <= 6 ? 2 : 3));
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

        OnLevelChange.Invoke();
    }

    public void Die()
    {
        //TP BACK PLAYER TO SPAWN
        m_PlayerController.Respawn(m_Levels[currentLevel]);
    }
}
