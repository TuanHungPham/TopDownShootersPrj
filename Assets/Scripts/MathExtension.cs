
using UnityEngine;

public static class MathVector
{
    public static Vector2 PerpendicularVector(Vector2 inputVector)
    {
        Vector2 perpendicularVector = Vector2.Perpendicular(inputVector);
        return perpendicularVector;
    }
}
