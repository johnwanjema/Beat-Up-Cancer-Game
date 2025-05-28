using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        if (SceneManager.GetActiveScene().name == "Level 1" && KillCounter.highScore1 < PointsCounter.totalScore)
        {
            KillCounter.highScore1 = PointsCounter.totalScore;
        }
        
        if (SceneManager.GetActiveScene().name == "Level 2" && KillCounter.highScore2 < PointsCounter.totalScore)
        {
            KillCounter.highScore2 = PointsCounter.totalScore;
        }

        if (SceneManager.GetActiveScene().name == "Level 3" && KillCounter.highScore3 < PointsCounter.totalScore)
        {
            KillCounter.highScore3 = PointsCounter.totalScore;
        }                
        KillCounter.kills = 0;
        KillCounter.boostedKills = 0;
        SceneManager.LoadScene(sceneID);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
