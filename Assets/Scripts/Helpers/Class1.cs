using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAfterTime:MonoBehaviour
{
    [SerializeField]
    private float loadDelay = 5f;

    private void Start()
    {
        Invoke("LoadNextScene", loadDelay);
    }

    private void LoadNextScene() 
    {
        SceneManager.LoadScene("NextSceneNameHere");
    }
}