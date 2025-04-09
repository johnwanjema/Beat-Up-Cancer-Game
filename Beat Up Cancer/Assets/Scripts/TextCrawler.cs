using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextCrawler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float _scrollSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Camera.main.transform.up * _scrollSpeed * Time.deltaTime);
    }
}
