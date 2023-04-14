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

        stepSize = 0.1f;
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

        Vector2 inputVector = new Vector2(x2 - x1, y2 - y1);

        // Tim vector vuong goc voi vector tao boi diem A vs B
        Vector2 perpendicularVector = MathVector.PerpendicularVector(inputVector);
        Debug.Log($"Vector: {JsonUtility.ToJson(perpendicularVector)}");
        // Tim trung diem cua AB(M)
        Vector2 midPoint = (startPoint + endPoint) / 2;
        // Tim vector MI vuong goc vector AB, MI co do dai distanceToVertex --> I
        // Tim dinh cua parabol
        Vector2 perpendicularVector_normalize = perpendicularVector.normalized;
        vertex = midPoint + perpendicularVector_normalize * distanceToVertex;
        // Tao parabol di qua diem A B va dinh vua tim dc

        float a = (y1 - vertex.y) / Mathf.Pow(x1 - vertex.x, 2);
        float b = -2 * a * vertex.x;
        float c = y1 - a * x1 * x1 - b * x1;

        if (x2 > x1)
        {
            for (float x = x1; x < x2; x += stepSize)
            {
                float y = a * x * x + b * x + c;
                listOfPoint.Add(new Vector3(x, y, 0));
            }
        }
        else if (x2 < x1)
        {
            for (float x = x1; x > x2; x -= stepSize)
            {
                float y = -(a * x * x + b * x + c);
                listOfPoint.Add(new Vector3(x, y, 0));
            }
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

    private Vector2 GetPerpendicularVector(Vector2 inputVector)
    {
        Vector2 newVector = Vector2.Perpendicular(inputVector);
        return newVector;
    }

    public void HideTrajectory()
    {
        listOfPoint.Clear();
        lineRenderer.positionCount = 0;
    }
}
