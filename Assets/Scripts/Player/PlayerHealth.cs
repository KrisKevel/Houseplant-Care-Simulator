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
        if (_stress + stress > 100) {
            _stress = 100f;
        } else if (_stress + stress < 0) {
            _stress = 0f;
        } else {
            _stress += stress; 
        }

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
