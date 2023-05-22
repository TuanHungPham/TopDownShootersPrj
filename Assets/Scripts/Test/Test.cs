using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject obj;
    public Transform point1;
    public Transform point2;
    public Vector3 startPoint => point1.position;
    public Vector3 endPoint => point2.position;
    public Vector3 gravity;
    public float vel;
    public float height;
    public float angle;
    public float time;
    public float timeStep;
    public float distance;
    public LineRenderer lineRenderer;
    public List<Vector3> listOfPoint = new List<Vector3>();
    public List<Vector3> lastListOfPoint = new List<Vector3>();

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        DrawThrowLine();
    }

    private void DrawThrowLine()
    {
        distance = Vector3.Distance(startPoint, endPoint);

        lastListOfPoint.Clear();
        Vector3 newEndPoint = new Vector3(endPoint.x - startPoint.x, endPoint.y - startPoint.y);

        float newHeight;
        CalculateHeight(endPoint, distance, out newHeight);

        gravity = new Vector3(0, -9.81f);
        float g = gravity.magnitude;

        CalculatePathWithHeight(newEndPoint, g, newHeight, out time, out angle, out vel);

        for (float t = 0; t <= time; t += timeStep)
        {
            float x = startPoint.x + GetX(t);
            float y = startPoint.y + GetY(g, t);
            listOfPoint.Add(new Vector3(x, y));
            lastListOfPoint.Add(new Vector3(x, y));
        }
        // Log(listOfPoint);

        lineRenderer.positionCount = listOfPoint.Count;
        lineRenderer.SetPositions(listOfPoint.ToArray());
        listOfPoint.Clear();
    }

    public void Throw()
    {
        Instantiate(obj, transform.position, transform.rotation);
        TestScore.Instance.score++;
        obj.SetActive(true);
    }

    void Log(List<Vector3> list)
    {
        foreach (var point in list)
        {
            var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.position = new Vector3(point.x, 0, point.y);
        }
    }

    private float GetY(float g, float t)
    {
        return (vel * Mathf.Sin(angle) * t) - (g * t * t) / 2;
    }

    private float GetX(float t)
    {
        return t * vel * Mathf.Cos(angle);
    }

    private float GetTime(Vector3 targetPoint, float vel, float angle)
    {
        return targetPoint.x / (vel * Mathf.Cos(angle));
    }

    private float QuadraticEquation(float a, float b, float c, float sign)
    {
        return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    }

    private void CalculatePathWithHeight(Vector3 targetPos, float gravity, float height, out float time, out float angle, out float vel)
    {
        float a = -(gravity / 2);
        float b = Mathf.Sqrt(2 * gravity * height);
        float c = -(endPoint.y);

        float t1 = QuadraticEquation(a, b, c, 1);
        float t2 = QuadraticEquation(a, b, c, -1);

        time = t1 > t2 ? t1 : t2;

        angle = Mathf.Atan((b * time) / targetPos.x);

        vel = b / Mathf.Sin(angle);
    }

    private void CalculateHeight(Vector3 targetPoint, float distance, out float height)
    {
        height = targetPoint.y + this.height / distance;
        height = Mathf.Max(1f, height);
    }

    private void CalculateVelocity(Vector3 targetPoint, float gravity, float angle)
    {
        float a = targetPoint.x * targetPoint.x * gravity;
        float b = 2 * targetPoint.x * Mathf.Sin(angle) * MathF.Cos(angle);
        float c = 2 * targetPoint.y * MathF.Cos(angle) * MathF.Cos(angle);

        vel = Mathf.Sqrt(a / (b - c));
    }
}
