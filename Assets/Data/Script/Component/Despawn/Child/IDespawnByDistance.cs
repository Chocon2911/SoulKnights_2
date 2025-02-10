using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDespawnByDistance : IDespawner
{
    Transform GetTarget(DespawnByDistance component);
}
