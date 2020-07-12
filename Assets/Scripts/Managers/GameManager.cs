using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int maxCitizen = 67;
    [SerializeField] private Text citizenText;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            CloseApp();

        if (Input.GetKeyDown(KeyCode.M))
            ToggleMusic();
    
    }

    //Keep track of citi.
    public void AddCitizen() 
    {
        
    }

    //Close app.
    void CloseApp() 
    {
        Application.Quit();
    }

    //Toggle Music.
    void ToggleMusic() 
    {
        musicSource.enabled = !musicSource.enabled;
    }
}
