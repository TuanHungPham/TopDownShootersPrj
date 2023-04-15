using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeTrajectory : MonoBehaviour
{
    #region  public var
    public List<Vector3> listOfPoint = new List<Vector3>();
    public List<Vector3> lastListOfPoint = new List<Vector3>();
    public float stepSize;
    public float heigthPlus;
    #endregion

    #region private var
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Vector3 endPoint;
    [SerializeField] private Vector3 vertex;
    [SerializeField] private LineRenderer lineRenderer;
    private float vel;
    private float angle;
    private float time;
    private Vector3 gravity;
    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        lineRenderer = GetComponent<LineRenderer>();

        stepSize = 0.1f;
    }

    private void Update()
    {
        GetPoint();
        DrawGeneradeTrajectory();
    }

    private void GetPoint()
    {
        startPoint = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter").position;
        endPoint = transform.parent.Find("GrenadeDmgAreaBG").position;
    }

    private Vector3 GetLastPoint()
    {
        Vector3 lastPoint = new Vector3(endPoint.x - startPoint.x, endPoint.y - startPoint.y);
        return lastPoint;
    }

    private void DrawGeneradeTrajectory()
    {
        lastListOfPoint.Clear();
        listOfPoint.Clear();

        float distance = Vector3.Distance(startPoint, endPoint);

        Vector3 lastPoint = GetLastPoint();

        float newHeight;
        PhysicExtension.CalculateHeight(startPoint, endPoint, distance, heigthPlus, out newHeight);

        gravity = new Vector3(0, -9.81f);
        float g = gravity.magnitude;

        PhysicExtension.CalculatePathWithHeight(lastPoint, g, newHeight, out time, out angle, out vel);

        for (float t = 0; t <= time; t += stepSize)
        {
            float x = startPoint.x + PhysicExtension.GetX(t, vel, angle);
            float y = startPoint.y + PhysicExtension.GetY(g, t, vel, angle);
            listOfPoint.Add(new Vector3(x, y));
            lastListOfPoint.Add(new Vector3(x, y));
        }

        lineRenderer.positionCount = listOfPoint.Count;
        lineRenderer.SetPositions(listOfPoint.ToArray());

    }

    public void HideTrajectory()
    {
        listOfPoint.Clear();
        lineRenderer.positionCount = 0;
    }
}
