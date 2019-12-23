using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    public static MouseTracker mt;
    Transform target;
    // Start is called before the first frame update
    void Awake()
    {
        if (mt != null)
            Destroy(gameObject);
        else
            mt = this;
    }

    void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.gameGoingOn)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 offset = mousePos - (Vector2)target.position;
            float dist = Vector2.Distance(mousePos, target.position) * 1;
            offset.Normalize();
            transform.position = new Vector3(target.position.x + offset.x * dist, target.position.y + offset.y * dist, transform.position.z);
        }
    }
}
