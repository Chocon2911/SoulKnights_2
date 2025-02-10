using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetector
{
    bool CanDetect(Detector component);
}
