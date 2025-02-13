using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDespawnByHealth : IDespawner
{
    int GetCurrHealth(DespawnByHealth component);
}
