using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Awake()
    {
        Events.OnGameOver += ShowWindow;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnGameOver -= ShowWindow;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void ShowWindow()
    {
        gameObject.SetActive(true);
    }
}
