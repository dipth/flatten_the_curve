using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScene : MonoBehaviour
{
    public float countdownTimer = 4f;

    private void Awake()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(countdownTimer);
        SceneManager.LoadScene("Title");
    }
}
