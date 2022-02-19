using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resource : MainBehaviour
{
    [SerializeField] protected TextMeshProUGUI resourceText;

    // Gán resourceText.text với thông số truyền vào value
    public virtual void SetResText(string value)
    {
        resourceText.text = value;
    }
}
