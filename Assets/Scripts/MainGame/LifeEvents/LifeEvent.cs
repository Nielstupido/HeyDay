using System.Collections;
using System.Collections.Generic;

public abstract class LifeEvent
{
    public string message;
    public abstract void TriggerLifeEvent();
}
