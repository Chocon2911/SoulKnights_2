using System.Collections;
using UnityEngine;


public interface IDetectOnStay : IDetector
{
    bool CanRecharge(DetectOnStay component);
}