using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum Affiliation
{
    None   = 0,

    Player = 1 << 0, // после << указывается сдвиг по битам (читается с конца). типа 0001
    Demon  = 1 << 1,
    Netral = 1 << 2,

   
    Everything = int.MaxValue
}
