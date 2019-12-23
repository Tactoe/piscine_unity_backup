using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int targetHoleIndex = 1;
    public int hits;

    Rigidbody rb;
    Vector3[] holeStartPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Transform[] tmp = GameObject.FindWithTag("HoleStartPosition").GetComponentsInChildren<Transform>();
        holeStartPosition = new Vector3[tmp.Length - 1];
        for (int i = 1; i < tmp.Length; i++)
            holeStartPosition[i - 1] = tmp[i].position;
        transform.position = holeStartPosition[targetHoleIndex - 1];
    }

    void StopVelocity()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(CheckWhenToStop());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hole" + targetHoleIndex)
        {
            if (targetHoleIndex + 1 <= holeStartPosition.Length)
                UiManager.um.ShowVictoryScreen();
            targetHoleIndex++;
            if (targetHoleIndex > holeStartPosition.Length)
            {
                UiManager.um.FinalScreen();
            }
            else
            {
                rb.constraints = RigidbodyConstraints.None;
                transform.position = holeStartPosition[targetHoleIndex - 1];
            }
            Time.timeScale = 0;
        }
    }


    IEnumerator CheckWhenToStop()
    {
        while (rb.velocity.magnitude > 0.8f)
        {
            yield return new WaitForSeconds(0.5f);
        }
        StopVelocity();
    }
}
