using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    public TMP_Text displayHighScore1, displayHighScore2, displayHighScore3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displayHighScore1.text = string.Format("{0}", KillCounter.highScore1);
        displayHighScore2.text = string.Format("{0}", KillCounter.highScore2);
        displayHighScore3.text = string.Format("{0}", KillCounter.highScore3);
    }
}
