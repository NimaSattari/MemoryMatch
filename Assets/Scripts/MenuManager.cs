using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    LevelScriptableObject[] levelConfigs;
    [SerializeField] Button levelButtonPrefab;
    [SerializeField] Transform levelsPanel;
    private void Awake()
    {
        levelConfigs = Resources.LoadAll<LevelScriptableObject>("Configs");
    }

    private void Start()
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
