using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private void Awake()
    {
        Events.OnWin += ShowWindow;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnWin -= ShowWindow;
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
