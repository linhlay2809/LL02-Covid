using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Notify", menuName = "Scriptable Object/Notify")]
public class NotifySO : ScriptableObject
{
    public NotifyName notifyName;
    [TextArea()]
    public string content;
}
