using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPanel : MonoBehaviour
{
    private HouseplantHealth _houseplant;

    void Awake()
    {
        Events.OnOpenDeadPanel += OpenPanel;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnOpenDeadPanel -= OpenPanel;
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
}
