using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickUpItem
{
    bool CanPickUp(PickUpItem component);
    Inventory GetInventory(PickUpItem component);
}
