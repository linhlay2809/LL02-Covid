using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRResource : Resource
{

    public override void SetResText(float value)
    {
        resourceText.text = value + " / 100%";
    }
}
