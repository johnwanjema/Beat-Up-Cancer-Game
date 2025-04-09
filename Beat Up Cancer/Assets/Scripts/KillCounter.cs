using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public TMP_Text killCounter;
    public static int kills = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        killCounter.text = string.Format("Cancer Cells Killed: {0}/10", kills);
    }
}
