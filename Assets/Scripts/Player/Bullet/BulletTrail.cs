using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private float speed;
    private Vector3 startPoint;
    private Vector3 targetPoint;
    private float progress;
    #endregion

    private void Start()
    {
        startPoint = new Vector3(transform.position.x, transform.position.y, -1);
    }

    private void Update()
    {
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPoint, targetPoint, progress);
    }

    public void SetTargetPoint(Vector3 pos)
    {
        targetPoint = new Vector3(pos.x, pos.y, -1);
    }
}
