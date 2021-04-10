using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Win : MonoBehaviour
{
    public TextMeshProUGUI WinDay;
    public TextMeshProUGUI MessageText;
    public Image SadEricImage;
    public Image NeutralEricImage;
    public Image HappyEricImage;

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
        GameManager.Instance.UnpauseGame();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void ShowWindow()
    {
        HappyEricImage.gameObject.SetActive(false);
        NeutralEricImage.gameObject.SetActive(false);
        SadEricImage.gameObject.SetActive(false);

        WinDay.text = GameManager.Instance.WinDay.ToString();

        float stress = GameManager.Instance.GetStress();

        if (stress < 33.33)
        {
            MessageText.text = Message.LowStress;
            HappyEricImage.gameObject.SetActive(true);
        }
        else if (stress < 66.66)
        {
            MessageText.text = Message.MediumStress;
            NeutralEricImage.gameObject.SetActive(true);
        }
        else
        {
            MessageText.text = Message.HighStress;
            SadEricImage.gameObject.SetActive(true);
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
