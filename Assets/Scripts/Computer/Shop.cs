using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
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
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
        Events.OpenWelcomeScreen();
    }
}
