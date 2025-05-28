using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        KillCounter.kills = 0;
        KillCounter.boostedKills = 0;
        SceneManager.LoadScene(sceneID);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
