using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public int ShowForSeconds = 5;

    public void ShowNotification()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowAndHide());
    }

    private IEnumerator ShowAndHide()
    {
        yield return new WaitForSeconds(ShowForSeconds);

        gameObject.SetActive(false);
    }
}
