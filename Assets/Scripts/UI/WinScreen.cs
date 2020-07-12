using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] Image heartImage;
    [SerializeField] Text promptText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartHeartAnimation());
        StartCoroutine(StartTextAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Briefing");
        } else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private IEnumerator StartHeartAnimation()
    {
        float speed = 1.5f;
        float scale = 0.2f;

        while (true)
        {
            iTween.ScaleAdd(heartImage.gameObject, iTween.Hash("y", scale, "x", scale, "time", speed, "easyType", "easeOutQuad"));
            yield return new WaitForSeconds(speed - 0.5f);
            iTween.ScaleAdd(heartImage.gameObject, iTween.Hash("y", -scale, "x", -scale, "time", speed, "easyType", "easeInQuad"));
            yield return new WaitForSeconds(speed - 0.5f);
        }
    }

    private IEnumerator StartTextAnimation()
    {
        float speed = 1.5f;
        float scale = 0.1f;

        while (true)
        {
            iTween.ScaleAdd(promptText.gameObject, iTween.Hash("y", scale, "x", scale, "time", speed, "easyType", "easeOutQuad"));
            yield return new WaitForSeconds(speed - 0.5f);
            iTween.ScaleAdd(promptText.gameObject, iTween.Hash("y", -scale, "x", -scale, "time", speed, "easyType", "easeInQuad"));
            yield return new WaitForSeconds(speed - 0.5f);
        }
    }
}
