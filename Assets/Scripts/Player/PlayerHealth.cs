using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float _stress;

    private void Awake()
    {
        Events.OnToggleSleep += MorningStressCheck;
        Events.OnUpdateStressLevel += UpdateStressLevel;
    }

    void Start()
    {
        _stress = GameManager.Instance.InitialStress;
        Events.UpdateStressUI(_stress);
    }

    private void OnDestroy()
    {
        Events.OnUpdateStressLevel -= UpdateStressLevel;
        Events.OnToggleSleep -= MorningStressCheck;
    }

    private void UpdateStressLevel(float stress)
    {
        _stress = Mathf.Clamp(_stress + stress, 0, 100);

        Events.UpdateStressUI(_stress);
    }

    void MorningStressCheck(bool sleeping)
    {
        if (!sleeping && _stress == 100f)
        {
            Events.GameOver();
        }
    }
}
