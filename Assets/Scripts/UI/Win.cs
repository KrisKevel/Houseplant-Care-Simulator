using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Win : MonoBehaviour
{
    public TextMeshProUGUI WinDay;

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
        WinDay.text = GameManager.Instance.WinDay.ToString();
        gameObject.SetActive(true);
    }
}
