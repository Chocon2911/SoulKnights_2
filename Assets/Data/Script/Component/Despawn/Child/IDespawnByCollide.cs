using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDespawnByCollide : IDespawner
{
    CapsuleCollider2D GetCollider(DespawnByCollide component);
}
