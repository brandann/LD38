using UnityEngine;
using System.Collections;

public static class NMath {

    public enum LeftRight { Left = -1, Vertical = 0, Right = 1 };
    public static LeftRight GetLeftRight(Transform activeObject, Transform otherObject)
    {
        var relativePoint = activeObject.InverseTransformPoint(otherObject.position);
        if (relativePoint.x < 0.0)
        {
            return LeftRight.Left;
        }
        else if (relativePoint.x > 0.0)
        {
            return LeftRight.Right;
        }
        return LeftRight.Vertical;
    }

    public enum FrontBack { Front, Horizontal, Back };
    public static FrontBack GetFrontBack(Transform activeObject, Transform otherObject)
    {
        var v = activeObject.InverseTransformPoint(otherObject.position);
        v.Normalize();
        float dot = Vector3.Dot(otherObject.up, v);
        if (dot < 0.0)
        {
            return FrontBack.Back;
        }
        else if (dot > 0.0)
        {
            return FrontBack.Front;
        }
        return FrontBack.Horizontal;
    }
}
