using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Header;
    public string Content;

    private Coroutine _delay;

    private void OnDisable()
    {
        Hide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _delay = StartCoroutine(ShowTooltip());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hide();
    }

    public IEnumerator ShowTooltip()
    {
        yield return new WaitForSeconds(0.5f);

        TooltipSystem.Show(Content, Header);
    }

    private void Hide()
    {
        if (_delay != null)
        {
            StopCoroutine(_delay);
            TooltipSystem.Hide();
        }
    }
}
