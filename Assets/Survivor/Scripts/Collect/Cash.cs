using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCN.Common;
using System;
public class Cash : DroppableCurrency, ICollectable
{
    protected override void Collected()
    {
        RemoveObj();
    }
}
