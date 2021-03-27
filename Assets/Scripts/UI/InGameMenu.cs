using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private void Awake()
    {
        Events.OnOpenMenu += OpenMenu;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Continue();
        }
    }

    private void OnDestroy()
    {
        Events.OnOpenMenu -= OpenMenu;
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    public void Continue()
    {
        gameObject.SetActive(false);
        GameManager.Instance.UnpauseGame();
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
