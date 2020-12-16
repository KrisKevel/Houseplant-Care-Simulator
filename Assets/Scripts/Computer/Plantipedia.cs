using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantipedia : MonoBehaviour
{
    void Awake()
    {
        Events.OnOpenPlantipedia += OpenPanel;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnOpenPlantipedia -= OpenPanel;
    }

    void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    public void ClosePlantipedia()
    {
        gameObject.SetActive(false);
        Events.OpenWelcomeScreen();
    }
}
