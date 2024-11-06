using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public Action<int> PlayerOnHit;
    public Action GameOver;
    public Action<int> PunchInCrease;

    public Action AssetAllLoaded;

}
