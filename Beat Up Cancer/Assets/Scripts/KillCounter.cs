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
    private static float doublePointsTimer = 0f;

    void Update()
    {
        // --- Timer logic for doublePoints ---
        if (doublePoints)
        {
            doublePointsTimer -= Time.deltaTime;
            if (doublePointsTimer <= 0f)
            {
                doublePoints = false;
                multiplier = 1;
                doublePointsTimer = 0f;
            }
        }

        // --- UI updates ---
        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            killCounter.text = $"Cancer Cells Killed: {kills}/1";
            ingameScore.text = $"Score: {kills * 50000 + boostedKills * 50000}";

            if (kills >= 1)
            {
                if (highScore3 < PointsCounter.totalScore)
                    highScore3 = PointsCounter.totalScore;

                VictoryPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else
        {
            killCounter.text = $"Cancer Cells Killed: {kills}/10";
            ingameScore.text = $"Score: {kills * 500 + boostedKills * 500}";

            if (kills >= 10)
            {
                if (SceneManager.GetActiveScene().name == "Level 1" && highScore1 < PointsCounter.totalScore)
                    highScore1 = PointsCounter.totalScore;

                if (SceneManager.GetActiveScene().name == "Level 2" && highScore2 < PointsCounter.totalScore)
                    highScore2 = PointsCounter.totalScore;

                VictoryPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }

        // Update score multiplier UI
        scoreMultiplier.text = $"x{multiplier}";

        // Update high score UI based on current level
        if (SceneManager.GetActiveScene().name == "Level 1")
            highScoreUI.text = $"{highScore1}";
        else if (SceneManager.GetActiveScene().name == "Level 2")
            highScoreUI.text = $"{highScore2}";
        else if (SceneManager.GetActiveScene().name == "Level 3")
            highScoreUI.text = $"{highScore3}";
    }

    public static void ActivateDoublePoints(float duration)
    {
        doublePoints = true;
        multiplier = 2;
        doublePointsTimer = duration;
    }
}
