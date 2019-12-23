using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMovingPath : MonoBehaviour
{
    public List<Vector3> pathPoints;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Refresh() {
        if (pathPoints == null)
            pathPoints = new List<Vector3>();
        pathPoints.Clear();

        foreach (Transform childTransform in transform.GetComponentsInChildren<Transform>())
        {
            if (transform == childTransform)
                continue;
            pathPoints.Add(childTransform.position);
        }
    }

    private void OnTransformChildrenChanged()
    {
        Refresh();
    }

    private void OnDrawGizmos()
    {
        Refresh();
        Gizmos.color = Color.black;
        for (int i = 0; i < pathPoints.Count; i++)
        {
            Gizmos.DrawSphere(pathPoints[i], 0.1f);
            if (i > 0)
                Gizmos.DrawLine(pathPoints[i], pathPoints[i - 1]);
        }
    }
}
