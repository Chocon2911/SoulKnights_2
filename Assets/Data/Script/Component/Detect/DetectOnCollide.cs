using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectOnCollide : DetectByCollide
{
    //===========================================Unity============================================
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        this.Detecting(collision);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        this.targets.Remove(collision.transform);
    }
}
