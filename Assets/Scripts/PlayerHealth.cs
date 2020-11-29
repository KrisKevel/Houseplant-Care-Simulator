using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Range(0, 100)]
    public float Stress = 20f;

    // Start is called before the first frame update
    void Start()
    {
        Events.OnUpdateStressLevel += UpdateStressLevel;

        UpdateStressLevel(0);
    }

    private void OnDestroy()
    {
        Events.OnUpdateStressLevel -= UpdateStressLevel;
    }

    private void UpdateStressLevel(float stress)
    {
        if (Stress + stress > 100) { 
            Stress = 100f; 
        } else if (Stress + stress < 0) { 
            Stress = 0f; 
        } else { 
            Stress += stress; 
        }

        Events.UpdateStressUI(Stress);
    }
}
