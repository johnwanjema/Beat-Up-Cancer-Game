using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class KillCounter : MonoBehaviour
{
    public TMP_Text killCounter;
    public TMP_Text ingameScore;
    public GameObject VictoryPanel;
    public static int kills = 0;
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
            ingameScore.text = string.Format("Score: {0}", kills * 500);
            if (kills >= 99)
            {
                VictoryPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }

        else
        {
            killCounter.text = string.Format("Cancer Cells Killed: {0}/10", kills);
            ingameScore.text = string.Format("Score: {0}", kills * 500);
            if (kills >= 10)
            {
                VictoryPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}