
using UnityEngine;

public static class PhysicExtension
{
    public static float GetY(float g, float t, float vel, float angle)
    {
        return (vel * Mathf.Sin(angle) * t) - (g * t * t) / 2;
    }

    public static float GetX(float t, float vel, float angle)
    {
        return t * vel * Mathf.Cos(angle);
    }

    public static float GetTime(Vector3 targetPoint, float vel, float angle)
    {
        return targetPoint.x / (vel * Mathf.Cos(angle));
    }

    public static float QuadraticEquation(float a, float b, float c, float sign)
    {
        return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    }

    public static void CalculatePathWithHeight(Vector3 targetPos, float gravity, float height, out float time, out float angle, out float vel)
    {
        float a = -(gravity / 2);
        float b = Mathf.Sqrt(2 * gravity * height);
        float c = -(targetPos.y);

        float t1 = QuadraticEquation(a, b, c, 1);
        float t2 = QuadraticEquation(a, b, c, -1);

        time = t1 > t2 ? t1 : t2;

        angle = Mathf.Atan((b * time) / targetPos.x);

        vel = b / Mathf.Sin(angle);
    }

    public static void CalculateHeight(Vector3 startPoint, Vector3 targetPoint, float distance, float heigthPlus, out float height)
    {
        height = Mathf.Abs(targetPoint.y - startPoint.y) + heigthPlus / distance;
        height = Mathf.Max(1f, height);
        // if (distance > 0 && distance < 4.7f)
        // {
        //     height = 10.5f;
        // }
    }

    public static void CalculateVelocity(Vector3 targetPoint, float gravity, float angle, out float vel)
    {
        float a = targetPoint.x * targetPoint.x * gravity;
        float b = 2 * targetPoint.x * Mathf.Sin(angle) * Mathf.Cos(angle);
        float c = 2 * targetPoint.y * Mathf.Cos(angle) * Mathf.Cos(angle);

        vel = Mathf.Sqrt(a / (b - c));
    }
}
