using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCN.Common;
using System;

public class Candy : DroppableCurrency, ICollectable
{
    public static Action onCollect;
    protected override void Collected()
    {
        onCollect?.Invoke();
        RemoveObj();
    }
}
