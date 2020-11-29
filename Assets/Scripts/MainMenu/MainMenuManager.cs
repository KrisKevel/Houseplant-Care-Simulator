using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void StartTheGame()
    {
        SceneManager.LoadScene("HouseScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
