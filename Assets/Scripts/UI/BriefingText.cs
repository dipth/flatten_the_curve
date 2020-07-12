using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BriefingTextState
{
    Writing,
    Waiting
}

public class BriefingText : MonoBehaviour
{
    [SerializeField] float typeDelay = 0.1f;

    private BriefingTextState state = BriefingTextState.Waiting;
    private int currentLineIndex = -1;
    private string[] lines =
    {
        "MAYOR:\nDr. Q. Orantine, you're just the man I need!\nWe have a dangerous virus spreading throughout the town and the citizens are refusing to stay at home...",
        "MAYOR:\nI need you on the streets doctor to 'convince' these out of control people to go home...",
        "MAYOR:\nBring plenty of disinfectant and be careful not to get exposed yourself!...",
        "MAYOR:\nAnd one last thing doctor. I've been told that toilet paper can be used to get the attention of many citizens at the same time. Use it wisely...",
        "MAYOR:\nGood luck doctor. Now get out there!"
    };
    private Text textArea;
    private AudioSource audio;
    private Coroutine coroutine;

    // Start is called before the first frame update
    void Start()
    {
        textArea = GetComponent<Text>();
        audio = GetComponent<AudioSource>();
        textArea.text = "";
        StartWriting();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Action();
        }
    }

    private void Action()
    {
        if(state == BriefingTextState.Waiting && currentLineIndex < lines.Length - 1)
        {
            StartWriting();
        } else if(state == BriefingTextState.Writing)
        {
            StartWaiting();
        } else
        {
            GoToNextScene();
        }
    }

    private void StartWriting()
    {
        if(state != BriefingTextState.Waiting || currentLineIndex >= lines.Length - 1)
        {
            return;
        }

        state = BriefingTextState.Writing;
        currentLineIndex++;
        audio.Play();
        coroutine = StartCoroutine(PrintLine());
    }

    private void StartWaiting()
    {
        if(state != BriefingTextState.Writing)
        {
            return;
        }

        StopCoroutine(coroutine);
        audio.Stop();
        state = BriefingTextState.Waiting;
        textArea.text = lines[currentLineIndex];
    }

    private void GoToNextScene()
    {
        // TODO: Implement me
        Debug.Log("Going to next scene...");
    }

    private IEnumerator PrintLine()
    {
        string line = lines[currentLineIndex];
        for(int i = 0; i <= line.Length; i++)
        {
            textArea.text = line.Substring(0, i);
            yield return new WaitForSeconds(typeDelay);
        }

        audio.Stop();
        state = BriefingTextState.Waiting;
    }
}
