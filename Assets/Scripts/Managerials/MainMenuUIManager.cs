using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{

    public Slider difficultySlider;
    public GameObject controlsPanel;
    int difficultyIndex = 0;

    public void LoadGameScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        Time.timeScale = 1f;
        AsyncOperation operation = null;
        switch (difficultyIndex)
        {
            case 0:
                operation = SceneManager.LoadSceneAsync("Game_Easy");
                break;
            case 1:
                operation = SceneManager.LoadSceneAsync("Game_Hard");
                break;
            default:
                break;
        }
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void ShowControlsPanel()
    {
        controlsPanel.SetActive(true);
    }

    public void TurnOffControlsPanel()
    {
        controlsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeDifficulty()
    {
        difficultyIndex = Convert.ToInt32(difficultySlider.value);
    }
}
