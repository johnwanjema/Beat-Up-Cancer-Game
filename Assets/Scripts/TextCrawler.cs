using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TextCrawler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float _scrollSpeed = 20f;
    [SerializeField] private float _waitTime = 13f; // Time before scene loads

    [Tooltip("Name of the next scene to load")]
    [SerializeField] private string _nextSceneName;


    private void Start()
    {   
        Time.timeScale = 1f;
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Camera.main.transform.up * _scrollSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(_nextSceneName);
        }
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSecondsRealtime(_waitTime);

        if (!string.IsNullOrEmpty(_nextSceneName))
        {
            Time.timeScale = 1f; // Ensure normal time
            SceneManager.LoadScene(_nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not set on TextCrawler!");
        }
    }
}
