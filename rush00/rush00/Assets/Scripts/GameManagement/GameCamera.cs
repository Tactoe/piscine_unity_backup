using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{

    public static GameCamera gc;
    public float offsetStrength;
    Transform target;
    Vector3 initialPosition;
    float shakeDuration;
    float shakeMagnitude = 0.75f;

    void Awake()
    {
        if (gc != null)
            Destroy(gameObject);
        else
            gc = this;
    }

    void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public void SetShake(float newMagnitude)
    {
        shakeDuration = 0.1f;
        shakeMagnitude = newMagnitude;
    }

    void Update()
    {
        Vector2 offset = MouseTracker.mt.transform.position - target.position;
        float dist = Vector2.Distance(MouseTracker.mt.transform.position, target.position) * offsetStrength;
        offset.Normalize();
        transform.position = new Vector3(target.position.x + offset.x * dist, target.position.y + offset.y * dist, transform.position.z);
        if (shakeDuration > 0)
        {
            transform.position += Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime;
        }
    }
}
