using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public event Action<int> PlayerOnHit;
    public event Action PlayerDie;

    public event Action<int> MonsterOnHit;
}
