using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI FundsText;
    public TextMeshProUGUI Funds;

    void Awake()
    {
        Events.OnOpenShop += OpenPanel;
        Events.OnInsufficientFunds += NotifyOfInsufficientFunds;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnOpenShop -= OpenPanel;
        Events.OnInsufficientFunds -= NotifyOfInsufficientFunds;
    }

    void OpenPanel()
    {
        ChangeTextColor(new Color(255, 255, 255));
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

    void NotifyOfInsufficientFunds()
    {
        ChangeTextColor(new Color(255, 0, 0));
    }

    private void ChangeTextColor(Color color)
    {
        FundsText.color = color;
        Funds.color = color;
    }
}
