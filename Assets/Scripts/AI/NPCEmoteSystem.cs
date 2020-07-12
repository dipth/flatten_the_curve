using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEmoteSystem : MonoBehaviour
{
    public List<GameObject> emotes = new List<GameObject>();
    public NPCEmotions emotions;

    public void SetEmotion(NPCEmotions newEmotion) 
    {
        emotions = newEmotion;
        StartCoroutine(ShowEmotion());
    }

    private IEnumerator ShowEmotion() 
    {
        switch (emotions)
        {
            case NPCEmotions.Happy:
                emotes[0].SetActive(true);
                yield return new WaitForSeconds(3);
                emotes[0].SetActive(false);
                break;
            case NPCEmotions.Interacting:
                emotes[2].SetActive(true);
                yield return new WaitForSeconds(3);
                emotes[2].SetActive(false);
                break;
            case NPCEmotions.Confused:
                emotes[1].SetActive(true);
                yield return new WaitForSeconds(3);
                emotes[1].SetActive(false);
                break;
            default:
                break;
        }
    }
}

public enum NPCEmotions
{
    Happy,Interacting,Confused
}
