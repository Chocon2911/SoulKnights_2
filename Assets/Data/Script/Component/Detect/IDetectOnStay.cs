using System.Collections;
using UnityEngine;


public interface IDetectOnStay : IDetectByCollide
{
    bool CanRecharge(DetectOnStay component);
}