using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame() 
    {
        SceneManager.LoadScene("World");
    }

    public void OpenSettings() 
    {
    
    }

    public void CloseSettings() 
    {
    
    }

    public void OpenQuitPrompt() 
    {
    
    }

    public void CloseQuitPrompt() 
    {
    
    }

    public void CloseGame() 
    {
        Application.Quit();
    }
}
