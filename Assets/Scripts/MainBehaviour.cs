using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.LoadComponents();
    }
    protected virtual void Reset()
    {
        this.LoadComponents();
    }
    protected virtual void Update()
    {
        //For Overide
    }
    protected virtual void FixedUpdate()
    {
        //For Overide
    }
    protected virtual void LoadComponents()
    {
        //For Overide
    }
}
