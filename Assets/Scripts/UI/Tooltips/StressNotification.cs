using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressNotification : MonoBehaviour
{
    private void Awake()
    {
        
    }

    public void ShowNotification()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowAndHide());
    }

    private IEnumerator ShowAndHide()
    {
        yield return new WaitForSeconds(5);

        gameObject.SetActive(false);
    }
}
