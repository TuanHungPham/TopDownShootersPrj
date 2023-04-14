using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Vector2 inputVector;
    public Transform point1;
    public Transform point2;
    public Transform midPointTest;
    public Transform vertexPointTest;
    public Vector2 startPoint => point1.position;
    public Vector2 endPoint => point2.position;
    // public float distanceToVertex;
    // public Vector2 vertex;
    // public float speed;

    // private void TestIn2D()
    // {
    //     inputVector = endPoint - startPoint;

    //     Vector2 perpendicularVector = MathVector.PerpendicularVector(inputVector);
    //     if (perpendicularVector.y < 0)
    //     {
    //         perpendicularVector.y *= -1;
    //     }
    //     Debug.Log($"Vector: {JsonUtility.ToJson(perpendicularVector)}");

    //     // Tim trung diem cua AB(M)
    //     Vector2 midPoint = (startPoint + endPoint) / 2;
    //     midPointTest.position = midPoint;
    //     // Tim vector MI vuong goc vector AB, MI co do dai distanceToVertex --> I
    //     // Tim dinh cua parabol
    //     Vector2 perpendicularVector_normalize = perpendicularVector.normalized;
    //     vertex = midPoint + perpendicularVector_normalize * distanceToVertex;
    //     vertexPointTest.position = vertex;
    // }

    // private void TestIn3d()
    // {
    // Vector3 inputVector = endPoint - startPoint;

    // Vector3 perpendicularVector = MathVector.PerpendicularVector(inputVector);
    // if (perpendicularVector.y < 0)
    // {
    //     perpendicularVector.y *= -1;
    // }
    // Debug.Log($"Vector: {JsonUtility.ToJson(perpendicularVector)}");

    // // Tim trung diem cua AB(M)
    // Vector3 midPoint = (startPoint + endPoint) / 2;
    // midPointTest.position = midPoint;
    // // Tim vector MI vuong goc vector AB, MI co do dai distanceToVertex --> I
    // // Tim dinh cua parabol
    // Vector3 perpendicularVector_normalize = perpendicularVector.normalized;
    // var vertex = midPoint + perpendicularVector_normalize * distanceToVertex;
    // vertexPointTest.position = vertex;
    // }
    public Vector2 gravity;
    public float vel;
    public float height;
    public float time;
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
        ThrowLine();
    }

    private void Update()
    {
        // ThrowLine();
    }

    private void ThrowLine()
    {
        float distance = Vector2.Distance(startPoint, endPoint);
        gravity = new Vector2(0, -9.81f);
        float g = gravity.magnitude;

        Vector2 midPoint = (startPoint + endPoint) / 2;

        CalculateVelocity(distance, time, angle);
        CalculateHeight(g, angle);

        float maxY = 0;
        float t2 = 0;
        for (float t = 0; t <= 2 * time; t += timeStep)
        {
            float x = GetX(t);

            if (t >= time)
            {
                float y2 = GetY2(g, t2, height);
                // float y2 = maxY - ((x * x * g) / (2 * vel * vel * Mathf.Cos(angle) * Mathf.Cos(angle)));
                t2 += timeStep;
                listOfPoint.Add(new Vector3(x, y2));
                continue;
            }

            float y1 = GetY1(g, t);
            // float y1 = x * Mathf.Tan(angle) - ((x * x * g) / (2 * vel * vel * Mathf.Cos(angle) * Mathf.Cos(angle)));
            maxY = y1;
            listOfPoint.Add(new Vector3(x, y1));
        }

        Log(listOfPoint);

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

    private void CalculateHeight(float gravity, float angle)
    {
        height = (vel * vel * Mathf.Sin(angle) * Mathf.Sin(angle)) / (2 * gravity);
    }

    private void CalculateVelocity(float distance, float time, float angle)
    {
        vel = distance / (2 * time * Mathf.Cos(angle));
        Debug.Log("Vel0: " + vel);
        Debug.Log("Vel0 x VelY: " + (vel * Mathf.Sin(angle)));
    }
}
