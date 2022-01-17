using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{

    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverMessage;

    public void EnablePausePanel()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void DisablePausePanel()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        StartCoroutine(ReloadScene(false));
    }

    public void BackToHome()
    {
        StartCoroutine(ReloadScene(true));
    }

    public IEnumerator ReloadScene(bool isBackToMenu)
    {
        Time.timeScale = 1f;
        AsyncOperation asyncOperation = null;
        if (!isBackToMenu)
            asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        else
            asyncOperation = SceneManager.LoadSceneAsync("MainMenu");

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void GameOver(bool playerDied)
    {
        Time.timeScale = 0f;
        if(playerDied)
        {
            gameOverMessage.text = "Corgi Died :(";
        }
        else
        {
            gameOverMessage.text = "Corgi Got His Cherries !!";
        }
        gameOverPanel.SetActive(true);
    }

}
