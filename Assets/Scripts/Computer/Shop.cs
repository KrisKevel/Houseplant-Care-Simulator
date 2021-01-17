using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI Funds;

    void Awake()
    {
        Events.OnOpenShop += OpenPanel;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnOpenShop -= OpenPanel;
    }

    void OpenPanel()
    {
        gameObject.SetActive(true);
        UpdateFunds();
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
        Events.OpenWelcomeScreen();
    }

    public void UpdateFunds()
    {
        Funds.text = PlayerPrefs.GetFloat("Funds").ToString();
    }
}
