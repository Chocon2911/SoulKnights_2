using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDespawner
{
    bool CanDespawn(Despawner component);
    Spawner GetSpawner(Despawner component);
    Transform GetDespawnObj(Despawner component);
}
