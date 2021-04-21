using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Monitor : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool _mouseIsOver = false;

    void Awake()
    {
        Events.OnUseComputer += UseComputer;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnUseComputer -= UseComputer;
    }

    private void Update()
    {
        CheckDistanceToPlayer();
    }

    void UseComputer()
    {
        gameObject.SetActive(true);
        GameManager.Instance.UIPanelUp = true;
    }

    public void CloseMonitor()
    {
        gameObject.SetActive(false);
    }

    //https://answers.unity.com/questions/947856/how-to-detect-click-outside-ui-panel.html?page=1&pageSize=5&sort=votes 
    //Ziplock9000's answer + BluishGreenPro's comment
    public void OnDeselect(BaseEventData eventData)
    {
        if (!_mouseIsOver)
        {
            CloseMonitor();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseIsOver = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mouseIsOver = false;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    //https://answers.unity.com/questions/947856/how-to-detect-click-outside-ui-panel.html?page=1&pageSize=5&sort=votes 
    //Ziplock9000's answer + BluishGreenPro's comment

    public void CheckDistanceToPlayer()
    {
        bool closeEnough = Vector3.Distance(
            GameObject.Find("Player").transform.position,
            GameObject.Find("monitor").transform.position
            ) < GameManager.Instance.AOE;

        if (!closeEnough)
        {
            CloseMonitor();
        }
    }
}
