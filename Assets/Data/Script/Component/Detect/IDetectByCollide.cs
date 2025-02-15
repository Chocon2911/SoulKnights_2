using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetectByCollide : IDetector
{
    Transform GetOwner(DetectByCollide component);
}
