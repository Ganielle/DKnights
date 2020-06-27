using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemPickupCommand
{
    public abstract void Execute();

    public abstract bool IsFinished { get; }

    public abstract void StopIndicatorAnimation();
}
