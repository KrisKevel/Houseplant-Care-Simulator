using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeScreen : MonoBehaviour
{
    private void Awake()
    {
        Events.OnOpenWelcomeScreen += OpenWelcomeScreen;
    }

    private void OnDestroy()
    {
        Events.OnOpenWelcomeScreen -= OpenWelcomeScreen;
    }

    void OpenWelcomeScreen()
    {
        gameObject.SetActive(true);
    }

    void CloseWelcomeScreen()
    {
        gameObject.SetActive(false);
    }

    public void OnOpenShop()
    {
        Events.OpenShop();
        CloseWelcomeScreen();
    }

    public void OnOpenPlantipedia()
    {
        Events.OpenPlantipedia();
        CloseWelcomeScreen();
    }
}
