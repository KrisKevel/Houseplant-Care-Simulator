using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI HeaderField;
    public TextMeshProUGUI ContentField;
    public LayoutElement LayoutElement;

    public int CharacterWrapLimit;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            HeaderField.gameObject.SetActive(false);
        } 
        else
        {
            HeaderField.gameObject.SetActive(true);
            HeaderField.text = header;
        }

        ContentField.text = content;
        
        int headerLenght = HeaderField.text.Length;
        int contentLenght = ContentField.text.Length;

        LayoutElement.enabled = (headerLenght > CharacterWrapLimit || contentLenght > CharacterWrapLimit) ? true : false;
    }

    private void Update()
    {
        Vector2 position = Input.mousePosition;
        int x = (position.x < Screen.width / 2) ? 0 : 1;
        int y = (position.y < Screen.height / 2) ? 0 : 1;

        _rectTransform.pivot = new Vector2(x, y);
        transform.position = position;
    }
}
