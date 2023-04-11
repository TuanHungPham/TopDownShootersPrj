using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeTrajectory : MonoBehaviour
{
    #region  public var
    public List<Vector3> listOfPoint = new List<Vector3>();
    public float stepSize;
    public float distanceToVertex;
    #endregion

    #region private var
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Vector3 endPoint;
    [SerializeField] private Vector3 vertex;
    [SerializeField] private LineRenderer lineRenderer;
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

        stepSize = 0.2f;
        distanceToVertex = 5;
    }

    private void Update()
    {
        GetPoint();
        GenerateTrajectory();
    }

    private void GenerateTrajectory()
    {
        float x1 = startPoint.x;
        float y1 = startPoint.y;
        float x2 = endPoint.x;
        float y2 = endPoint.y;

        vertex = new Vector3((x1 + x2) / 2, y1 + distanceToVertex);

        float a = (y1 - vertex.y) / Mathf.Pow(x1 - vertex.x, 2);
        float b = (y1 * x2 * x2 - y2 * x1 * x1) / (x2 * x2 - x1 * x1);
        float c = y1 - a * x1 * x1 - b * x1;

        for (float x = x1; x < x2; x += stepSize)
        {
            float y = a * Mathf.Pow(x - vertex.x, 2) + vertex.y;
            listOfPoint.Add(new Vector3(x, y, 0));
        }

        lineRenderer.positionCount = listOfPoint.Count;
        lineRenderer.SetPositions(listOfPoint.ToArray());
        listOfPoint.Clear();
    }

    private void GetPoint()
    {
        startPoint = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter").position;
        endPoint = transform.parent.Find("GrenadeDmgAreaBG").position;
    }

    public void HideTrajectory()
    {
        listOfPoint.Clear();
        lineRenderer.positionCount = 0;
    }
}
