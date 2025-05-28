using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class KillCounter : MonoBehaviour
{
    public TMP_Text killCounter;
    public TMP_Text ingameScore;
    public TMP_Text scoreMultiplier;
    public TMP_Text highScoreUI;
    public TMP_Text displayHighScore1, displayHighScore2, displayHighScore3;
    public GameObject VictoryPanel;
    public static int kills = 0;
    public static int boostedKills = 0;
    public static int multiplier = 1;
    public static float highScore1, highScore2, highScore3 = 0;
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
            killCounter.text = string.Format("Cancer Cells Killed: {0}/99", kills);
            ingameScore.text = string.Format("Score: {0}", kills * 500 + boostedKills * 500);
            if (kills >= 99)
            {
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
        displayHighScore1.text = string.Format("{0}", highScore1);
        displayHighScore2.text = string.Format("{0}", highScore2);
        displayHighScore3.text = string.Format("{0}", highScore3);
    }
}