using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int maxCitizens = 67;
    private int savedCitizens = 0;
    [SerializeField] private Text totalCitizensLeftLabel;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        RefreshTotalCitizensLeftLabel();
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
        savedCitizens++;
        RefreshTotalCitizensLeftLabel();
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

    private void RefreshTotalCitizensLeftLabel()
    {
        totalCitizensLeftLabel.text = $"{savedCitizens}/{maxCitizens}";
    }
}
