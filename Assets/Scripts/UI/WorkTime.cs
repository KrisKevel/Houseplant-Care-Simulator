using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorkTime : MonoBehaviour
{
    private TextMeshProUGUI Time;

    private void Awake()
    {
        Events.OnToggleWork += TogglePanel;
        Time = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void OnDestroy()
    {
        Events.OnToggleWork -= TogglePanel;
    }

    private void Start()
    {
        TogglePanel(false);
    }

    private void Update()
    {
        Time.text = TimeManager.Instance.GetCurrentTime();
    }

    private void TogglePanel(bool toggle)
    {
        gameObject.transform.parent.gameObject.SetActive(toggle);
    }
}
