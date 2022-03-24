using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MainBehaviour
{
    [SerializeField] protected TextMeshProUGUI headerField;
    [SerializeField] protected TextMeshProUGUI contentField;
    [SerializeField] protected LayoutElement layoutElement;
    [SerializeField] protected int characterWrapLimit;

    [SerializeField] protected RectTransform rectTransform;

    protected override void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        if (headerLength > characterWrapLimit || contentLength > characterWrapLimit)
            layoutElement.enabled = true;
        else
            layoutElement.enabled = false;

    }
    
    protected override void Update()
    {
        if (Application.isEditor)
        {
            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;
            if (headerLength > characterWrapLimit || contentLength > characterWrapLimit)
                layoutElement.enabled = true;
            else
                layoutElement.enabled = false;
        }

        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;
        rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = position;
    }
}
