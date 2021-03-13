using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Header;
    public string Content;

    private Coroutine delay;

    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = StartCoroutine(ShowTooltip());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(delay);
        TooltipSystem.Hide();
    }

    public IEnumerator ShowTooltip()
    {
        yield return new WaitForSeconds(0.5f);

        TooltipSystem.Show(Content, Header);
    }
}
