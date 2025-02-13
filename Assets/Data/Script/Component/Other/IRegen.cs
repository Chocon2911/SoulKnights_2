using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRegen
{
    bool CanRegen(Regen component);
    void RestoreValue(Regen component);
}
