using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float ennemyDetectionAmount;
    public float runBonus;
    public GameObject door;
    public bool hasKey;

    float currentExposure = -1;
    AudioSource footStep;
    float cameraExposure = 45;
    GameObject steamObject;
    bool isHidden;
    // Start is called before the first frame update
    void Start()
    {
        steamObject = GameObject.FindWithTag("SteamObject");
        steamObject.SetActive(false);
        footStep = GetComponent<AudioSource>();
    }

    void TryInteracting()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward, Color.blue, 10);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 35))
        {

            if (hit.collider.gameObject.CompareTag("Keycard"))
            {
                hasKey = true;
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.gameObject.CompareTag("Fan"))
                steamObject.SetActive(true);
            else if (hit.collider.gameObject.CompareTag("Papers"))
                GameManager.gm.GameOver();
            else if (hit.collider.gameObject.CompareTag("Lock") && hasKey)
            {
                Destroy(door);
                hasKey = false;
            }
        }
    }

    void HandleInput()
    {
        float totalSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
            totalSpeed += runBonus;
        float xAxis = Input.GetAxis("Horizontal") * totalSpeed * Time.deltaTime;
        float zAxis = Input.GetAxis("Vertical") * totalSpeed * Time.deltaTime;

        if ((xAxis != 0 || zAxis != 0) && !footStep.isPlaying)
        {
            footStep.volume = Random.Range(0.8f, 1);
            footStep.pitch = Random.Range(0.8f, 1.1f);
            footStep.Play();
        }

        transform.Translate(new Vector3(xAxis, 0, zAxis));
        if (Input.GetKeyDown(KeyCode.E))
            TryInteracting();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        ennemyDetectionAmount += currentExposure * Time.deltaTime;
        if (ennemyDetectionAmount <= 0)
            currentExposure = 0;
        else if (ennemyDetectionAmount >= 100)
            GameManager.gm.GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CameraDetectionRange"))
        {
            if (isHidden)
                currentExposure = cameraExposure / 5;
            else
                currentExposure = cameraExposure;

        }
        if (other.gameObject.CompareTag("SteamObject"))
            isHidden = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CameraDetectionRange"))
            currentExposure = -cameraExposure / 5;
        if (other.gameObject.CompareTag("SteamObject"))
            isHidden = false;
    }

    void checkForObject()
    {

    }
}
