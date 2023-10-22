using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    LevelScriptableObject[] levelConfigs;

    [Header("Inspector Set Main Menu")]
    [SerializeField] Button levelButtonPrefab;
    [SerializeField] Transform levelsPanel;

    [Header("Inspector Set Game Scene")]
    [SerializeField] Button[] mainMenuButtons;
    [SerializeField] Button[] nextLevelButtons;
    private void Awake()
    {
        levelConfigs = Resources.LoadAll<LevelScriptableObject>("Configs");
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            PopulateLevelButtons();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SetGameButtons();
        }
    }

    private void SetGameButtons()
    {
        foreach (Button button in mainMenuButtons)
        {
            button.onClick.AddListener(() => SceneManager.LoadScene(0));
        }
        foreach (Button button in nextLevelButtons)
        {
            button.onClick.AddListener(() => LevelButtonOnClick(levelConfigs[Array.IndexOf(levelConfigs, CrossSceneData.levelConfig) + 1]));
        }
    }

    private void PopulateLevelButtons()
    {
        foreach (LevelScriptableObject level in levelConfigs)
        {
            Button levelButtonInstant = Instantiate(levelButtonPrefab, levelsPanel);
            levelButtonInstant.onClick.AddListener(() => LevelButtonOnClick(level));
        }
    }

    private void LevelButtonOnClick(LevelScriptableObject levelConfig)
    {
        CrossSceneData.levelConfig = levelConfig;
        SceneManager.LoadScene(1);
    }
}
