using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Monitor : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool _mouseIsOver = false;

    void Awake()
    {
        Events.OnUseComputer += OpenPanel;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnUseComputer -= OpenPanel;
    }

    void OpenPanel()
    {
        gameObject.SetActive(true);
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
            gameObject.SetActive(false);
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

    private void Update()
    {
        //print(_mouseIsOver);
    }
}
