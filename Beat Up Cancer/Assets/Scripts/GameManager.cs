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

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode){
        if(scene.name == "Level 1" || scene.name == "Level 2" ){
            Instantiate(players[charIndex]);
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
