﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int maxCitizens = 50;
    private int savedCitizens = 0;
    [SerializeField] private Text totalCitizensLeftLabel;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Image tutorialBox;
    [SerializeField] private Text tutorialText;

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

        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleHelp();
        }
    
    }

    //Keep track of citi.
    public void AddCitizen() 
    {
        savedCitizens++;
        RefreshTotalCitizensLeftLabel();
        if(savedCitizens >= maxCitizens)
        {
            SceneManager.LoadScene("Win");
        }
    }

    public void Die()
    {
        SceneManager.LoadScene("Loose");
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

    void ToggleHelp()
    {
        bool newValue = !tutorialBox.enabled;
        tutorialBox.enabled = newValue;
        tutorialText.enabled = newValue;
    }

    private void RefreshTotalCitizensLeftLabel()
    {
        totalCitizensLeftLabel.text = $"{savedCitizens}/{maxCitizens}";
    }
}
