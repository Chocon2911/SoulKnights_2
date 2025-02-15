using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetectByMouse : IDetector
{
    Transform GetMainObj(DetectByMouse component);
}
