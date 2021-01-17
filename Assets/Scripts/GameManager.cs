using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float StartingFunds = 100f;
    private void Awake()
    {
        PlayerPrefs.SetFloat("Funds", StartingFunds);
        Events.OnUpdateFunds += UpdateFunds;
    }

    private void OnDestroy()
    {
        Events.OnUpdateFunds -= UpdateFunds;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Events.OpenMenu();
        }
    }

    void UpdateFunds(float amount)
    {
        PlayerPrefs.SetFloat("Funds", PlayerPrefs.GetFloat("Funds") + amount);
    }
}
