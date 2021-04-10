using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private void Awake()
    {
        Events.OnToggleSleep += MorningStressCheck;
    }

    private void OnDestroy()
    {
        Events.OnToggleSleep -= MorningStressCheck;
    }

    void MorningStressCheck(bool sleeping)
    {
        if (!sleeping && GameManager.Instance.GetStress() == 100f)
        {
            GameManager.Instance.EndGame();
            Events.GameOver();
        }
    }
}
