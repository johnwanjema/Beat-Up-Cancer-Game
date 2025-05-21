using UnityEngine;
using TMPro;

public class PointsCounter : MonoBehaviour
{
    public TMP_Text pointsOutput;
    public static int levelClearPoints = 10000;
    public static int timePoints = 3000;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int totalScore = levelClearPoints + timePoints;
        pointsOutput.text = string.Format("{0}\n{1}\n{2}", levelClearPoints, timePoints, totalScore);
    }
}
