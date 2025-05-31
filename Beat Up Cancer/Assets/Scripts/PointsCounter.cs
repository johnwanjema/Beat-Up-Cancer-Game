using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PointsCounter : MonoBehaviour
{
    public TMP_Text pointsOutput;
    public static float levelClearPoints = 10000;
    public static float elimPoints = 0;
    public static float timePoints = 3000;
    public static float totalScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            levelClearPoints = 100000;
            elimPoints = KillCounter.kills * 25000 + KillCounter.boostedKills * 25000;
        }
        if (SceneManager.GetActiveScene().name != "Level 3")
        {
            levelClearPoints = 10000;
            elimPoints = KillCounter.kills * 500 + KillCounter.boostedKills * 500;
        }
        timePoints = Math.Max(0, (150 - ((Timer.minutes * 60) + Timer.seconds)) * 80);
        totalScore = levelClearPoints + elimPoints + timePoints;
        pointsOutput.text = string.Format("{0}\n{1}\n{2}\n{3}", levelClearPoints, elimPoints, timePoints, totalScore);
    }
}
