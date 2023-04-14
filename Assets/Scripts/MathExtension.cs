
using UnityEngine;

public static class MathVector
{
    public static Vector2 PerpendicularVector(Vector2 inputVector)
    {
        Vector2 perpendicularVector = Vector2.Perpendicular(inputVector);
        return perpendicularVector;
    }

    public static Vector3 PerpendicularVector(Vector3 inputVector)
    {
        Vector3 perpendicularVector = Vector3.Cross(inputVector, Vector3.back);
        return perpendicularVector;
    }
}
