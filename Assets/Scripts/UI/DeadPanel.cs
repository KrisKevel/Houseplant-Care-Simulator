using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeadPanel : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private HouseplantHealth _houseplant;
    private bool _mouseIsOver = false;

    void Awake()
    {
        Events.OnOpenDeadPanel += OpenPanel;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnOpenDeadPanel -= OpenPanel;
    }

    private void Update()
    {
        CheckDistanceToPlayer();
    }

    void OpenPanel(HouseplantHealth houseplant)
    {
        _houseplant = houseplant;
        transform.position = Input.mousePosition;
        gameObject.SetActive(true);
    }

    public void TossThePlant()
    {
        _houseplant.TossThePlant();
        gameObject.SetActive(false);
    }

    //https://answers.unity.com/questions/947856/how-to-detect-click-outside-ui-panel.html?page=1&pageSize=5&sort=votes 
    //Ziplock9000's answer + BluishGreenPro's comment
    public void OnDeselect(BaseEventData eventData)
    {
        if (!_mouseIsOver)
        {
            gameObject.SetActive(false);
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
            _houseplant.gameObject.transform.position
            ) < GameManager.Instance.AOE;

        if (!closeEnough)
        {
            gameObject.SetActive(false);
        }
    }
}
