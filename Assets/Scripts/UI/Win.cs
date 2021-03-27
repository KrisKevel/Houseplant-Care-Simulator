using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Win : MonoBehaviour
{
    public TextMeshProUGUI WinDay;
    public TextMeshProUGUI MessageText;

    public WinMessage Message;

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

        float stress = GameManager.Instance.GetStress();

        if (stress < 33.33)
        {
            MessageText.text = Message.LowStress;
        }
        else if (stress < 66.66)
        {
            MessageText.text = Message.MediumStress;
        }
        else
        {
            MessageText.text = Message.HighStress;
        }

        gameObject.SetActive(true);
        GameManager.Instance.PauseGame();
    }
}

[System.Serializable]
public class WinMessage
{
    public string LowStress;
    public string MediumStress;
    public string HighStress;
}
