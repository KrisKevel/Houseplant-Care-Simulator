using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressUIManager : MonoBehaviour
{

    private void Awake()
    {
        Events.OnUpdateStressUI += UpdateStress;

    }

    private void OnDestroy()
    {
        Events.OnUpdateStressUI -= UpdateStress;
    }

    private void UpdateStress(float stress)
    {
        gameObject.GetComponent<Image>().fillAmount = stress / 100;
    }
}
