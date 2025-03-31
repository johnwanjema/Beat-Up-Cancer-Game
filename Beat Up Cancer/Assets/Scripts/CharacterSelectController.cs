using UnityEngine;
using UnityEngine.SceneManagement;

public class characterSelect : MonoBehaviour
{
    public void PlayGame(){

        int selectedCharacter =
            int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        GameManager.instance.charIndex = selectedCharacter;
        Debug.Log("yes bana " +  GameManager.instance.charIndex);
       
        SceneManager.LoadScene("Level 1");
    
    }
}
