using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    Transform GetShooter(Bullet component);
    bool CanMove(Bullet component);
    
    // Charge
    bool CanStartCharge(Bullet component);
    bool CanFinishCharge(Bullet component);
}
