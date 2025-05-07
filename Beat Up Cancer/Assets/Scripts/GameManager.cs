using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int killStreak = 0;
    public int multiplier = 1;

    [SerializeField]
    private GameObject [] players;

    private int _charIndex;

    public int charIndex{
        get {return _charIndex; }
        set {_charIndex = value; }
    }

    private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    private void OnEnable(){
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
    if (scene.name == "Level 1" || scene.name == "Level 2") {
        Time.timeScale = 1f; // Ensure game is unpaused
   
        // Determine spawn position
        Vector3 spawnPos;
        Camera mainCam = Camera.main;
        if (mainCam != null) {
            spawnPos = mainCam.transform.position + new Vector3(0, 2, 5);
        } else {
            Debug.LogWarning("No Main Camera found. Using Vector3.zero.");
            spawnPos = Vector3.zero;
        }

        GameObject player = Instantiate(players[charIndex], spawnPos, Quaternion.identity);

        var controller = player.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = true;

        var rb = player.GetComponent<Rigidbody>();
        if (rb != null) {
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.WakeUp();
        }
        }
    }




    public void ScoreIncrement(int points){
        score += points * multiplier;   //add point amount of enemy with multiplier effect
        killStreak++;
        if(killStreak >= 5){            //multiplier increases every 5 kills without getting hit
            killStreak = 0;
            multiplier++;
        }
        print(score);                   //still need to display score, accessible as GameManager.instance.score
    }
}
