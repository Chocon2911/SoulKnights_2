using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSystemManager : HuyMonoBehaviour
{
    protected virtual void FixedUpdate()
    {
        SystemManager.Instance.LateFixedUpdate();
    }
}
