using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public float stoppingTime;
    public Transform path;

    Transform[] pathPoints;
    int nextPointIndex = 1;
    bool goingBack = false;
    float timerStopping;
    // Start is called before the first frame update
    void Start()
    {
        Transform[] tmp = path.transform.GetComponentsInChildren<Transform>();
        pathPoints = new Transform[tmp.Length - 1];
        for (int i = 1; i < tmp.Length; i++)
            pathPoints[i - 1] = tmp[i];
        transform.position = pathPoints[0].position;
        timerStopping = stoppingTime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = pathPoints[nextPointIndex].position - transform.position;
        Vector3 toGo = target.normalized * speed * Time.deltaTime;
        if (target.magnitude > 0.1f)
            transform.position += toGo;
        else
        {
            if (nextPointIndex - 1 >= 0 && nextPointIndex + 1 < pathPoints.Length)
                nextPointIndex += goingBack ? -1 : 1;
            else
            {
                if (timerStopping > 0)
                    timerStopping -= Time.deltaTime;
                else
                {
                    timerStopping = stoppingTime;
                    goingBack = !goingBack;
                    nextPointIndex += goingBack ? -1 : 1;
                }
            }
        }
    }
}
