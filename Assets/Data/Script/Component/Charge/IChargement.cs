using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargement : ISkill
{
    bool CanStart(Chargement component);
    bool CanFinishSkill(Chargement component);
}
