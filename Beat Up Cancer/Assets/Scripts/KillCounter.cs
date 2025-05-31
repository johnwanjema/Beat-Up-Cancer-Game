using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class KillCounter : MonoBehaviour
{
    public TMP_Text killCounter;
    public TMP_Text ingameScore;
    public TMP_Text scoreMultiplier;
    public TMP_Text highScoreUI;
    public GameObject VictoryPanel;
    public static int kills = 0;
    public static int boostedKills = 0;
    public static int multiplier = 1;
    public static float highScore1 = 0;
    public static float highScore2 = 0;
    public static float highScore3 = 0;
    public static bool doublePoints = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            killCounter.text = string.Format("Cancer Cells Killed: {0}/2", kills);
            ingameScore.text = string.Format("Score: {0}", kills * 25000 + boostedKills * 25000);
            if (kills >= 2)
            {                   
                if (highScore3 < PointsCounter.totalScore)
                {
                    highScore3 = PointsCounter.totalScore;
                }     
                VictoryPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }

        else
        {
            killCounter.text = string.Format("Cancer Cells Killed: {0}/10", kills);
            ingameScore.text = string.Format("Score: {0}", kills * 500 + boostedKills * 500);                  
            if (kills >= 10)
            {
                if (SceneManager.GetActiveScene().name == "Level 1" && highScore1 < PointsCounter.totalScore)
                {
                    highScore1 = PointsCounter.totalScore;
                }   
                if (SceneManager.GetActiveScene().name == "Level 2" && highScore2 < PointsCounter.totalScore)
                {
                    highScore2 = PointsCounter.totalScore;
                }   
                VictoryPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
        scoreMultiplier.text = string.Format("x{0}", multiplier);
        if (doublePoints)
        {
            multiplier = 2;
        }

        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            highScoreUI.text = string.Format("{0}", highScore1);
        }

        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            highScoreUI.text = string.Format("{0}", highScore2);
        }

        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            highScoreUI.text = string.Format("{0}", highScore3);
        }
    }
}