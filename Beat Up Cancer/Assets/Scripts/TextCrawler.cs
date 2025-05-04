using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TextCrawler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float _scrollSpeed = 20f;
    [SerializeField] private float _waitTime = 13f; // Time before scene loads

    private void Start()
    {
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Camera.main.transform.up * _scrollSpeed * Time.deltaTime);
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(_waitTime);

        SceneManager.LoadScene("Level 1");
    }
}
