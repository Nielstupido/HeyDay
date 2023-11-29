using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LifeEvent : MonoBehaviour
{
    public string message;
    public abstract void TriggerLifeEvent();
}
