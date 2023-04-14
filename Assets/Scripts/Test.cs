using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public Vector3 startPoint => point1.position;
    public Vector3 endPoint => point2.position;
    public Vector3 gravity;
    public float vel;
    public float height;
    public float angle;
    public float timeStep;
    public LineRenderer lineRenderer;
    public List<Vector3> listOfPoint = new List<Vector3>();

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        ThrowLine();
    }

    private void ThrowLine()
    {
        gravity = new Vector3(0, -9.81f);
        Vector3 vectorAB = endPoint - startPoint;
        Vector3 direction = vectorAB.normalized;
        Debug.Log("Dỉrection: " + direction);
        float distance = vectorAB.magnitude;
        float g = gravity.magnitude;

        CalculateVelocity(distance, g, angle);
        CalculateHeight(g, angle);

        float t1 = GetT1(vel, angle, g);
        float t2 = GetT2(height, g);

        float lastX = 0;
        float lastY = 0;
        for (float t = 0; t <= t1; t += timeStep)
        {
            float x = startPoint.x + GetX(t);
            lastX = x;
            float y = startPoint.y + GetY1(g, t);
            lastY = y;
            listOfPoint.Add(new Vector3(x, y));
        }

        for (float t = 0; t <= t2; t += timeStep)
        {
            float x = lastX + GetX(t);
            float y = GetY2(g, t, lastY);
            listOfPoint.Add(new Vector3(x, y));
        }

        // Log(listOfPoint);

        lineRenderer.positionCount = listOfPoint.Count;
        lineRenderer.SetPositions(listOfPoint.ToArray());
        listOfPoint.Clear();
    }


    void Log(List<Vector3> list)
    {
        foreach (var point in list)
        {
            // log.AppendLine(JsonUtility.ToJson(point));
            var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.position = new Vector3(point.x, 0, point.y);
        }
    }

    private static float GetY2(float g, float t, float height)
    {
        return height - (g * t * t) / 2;
    }

    private float GetY1(float g, float t)
    {
        return (vel * Mathf.Sin(angle) * t) - (g * t * t) / 2;
    }

    private float GetX(float t)
    {
        return t * vel * Mathf.Cos(angle);
    }

    private float GetT1(float vel, float angle, float gravity)
    {
        return (vel * Mathf.Sin(angle)) / gravity;
    }

    private float GetT2(float height, float gravity)
    {
        return Mathf.Sqrt((2 * height) / gravity);
    }

    private void CalculateHeight(float gravity, float angle)
    {
        height = (vel * vel * Mathf.Sin(angle) * Mathf.Sin(angle)) / (2 * gravity);
    }

    private void CalculateVelocity(float distance, float gravity, float angle)
    {
        vel = Mathf.Sqrt((distance * gravity) / Mathf.Sin(2 * angle));
    }
}
