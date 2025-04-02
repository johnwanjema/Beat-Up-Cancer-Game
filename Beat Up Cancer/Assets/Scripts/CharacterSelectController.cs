using UnityEngine;
using UnityEngine.SceneManagement;

public class characterSelect : MonoBehaviour
{
    public void PlayGame(){

        int selectedCharacter =
            int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        GameManager.instance.charIndex = selectedCharacter;
       
        SceneManager.LoadScene("Level 1");
    
    }
}
