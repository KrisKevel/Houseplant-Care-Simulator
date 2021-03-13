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
        if (Application.isEditor)
        {
            int headerLenght = HeaderField.text.Length;
            int contentLenght = ContentField.text.Length;

            LayoutElement.enabled = (headerLenght > CharacterWrapLimit || contentLenght > CharacterWrapLimit) ? true : false;
        }

        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;


        _rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }
}
