using UnityEngine;
using TMPro;

public class PointsCounter : MonoBehaviour
{
    public TMP_Text pointsOutput;
    public static float levelClearPoints = 10000;
    public static float elimPoints = 0;
    public static float timePoints = 3000;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elimPoints = KillCounter.kills * 500;
        timePoints = (150 - ((Timer.minutes * 60) + Timer.seconds)) * 80;
        float totalScore = levelClearPoints + elimPoints + timePoints;
        pointsOutput.text = string.Format("{0}\n{1}\n{2}\n{3}", levelClearPoints, elimPoints, timePoints, totalScore);
    }
}
