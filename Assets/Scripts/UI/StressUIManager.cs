using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressUIManager : MonoBehaviour
{

    private void Awake()
    {
        Events.OnUpdateStress += UpdateStress;
        Events.OnHourPassed += UpdateStress;

    }

    private void OnDestroy()
    {
        Events.OnUpdateStress -= UpdateStress;
        Events.OnHourPassed -= UpdateStress;
    }

    private void UpdateStress()
    {
        gameObject.GetComponent<Image>().fillAmount = GameManager.Instance.GetStress() / 100;
    }
}
