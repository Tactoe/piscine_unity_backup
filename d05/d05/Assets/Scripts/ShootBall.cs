using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShootBall : MonoBehaviour
{
    public float gaugeSpeed;
    public Vector3[] clubs;
    public string[] clubName;
    public float[] clubStrength;

    [SerializeField]bool isTryingToShoot;
    int increase = 1;
    int currentClub;
    float gaugeHeight = 1;
    float maxGaugeHeight;
    Image gaugeLevel;
    Text clubText;
    GameObject powerGauge;
    Rigidbody rb;
    Transform arrow;
    Vector3 currentGoal;
    Vector3 dirToGoal;
    GameObject ballGO;
    Ball ball;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        arrow = GetComponentsInChildren<Transform>()[1];
        ballGO = GameObject.FindWithTag("Ball");
        ball = ballGO.GetComponent<Ball>();
        powerGauge = GameObject.FindWithTag("PowerGauge");
        gaugeLevel = GameObject.FindWithTag("PowerLevel").GetComponent<Image>();
        clubText = GameObject.FindWithTag("ClubText").GetComponent<Text>();
        maxGaugeHeight = gaugeLevel.rectTransform.sizeDelta.y;
        currentGoal = GameObject.FindWithTag("Hole1").GetComponent<Transform>().position;
        TurnOffShootingMode();
        StartCoroutine(SetNewCameraPos());
    }

    void DetermineShotStrength()
    {
        if (gaugeHeight >= maxGaugeHeight || gaugeHeight <= 0)
            increase *= -1;
        gaugeHeight += gaugeSpeed * increase;
        gaugeLevel.rectTransform.sizeDelta = new Vector2(gaugeLevel.rectTransform.sizeDelta.x, gaugeHeight);
    }

    void TurnOffShootingMode()
    {
        gaugeHeight = 1;
        isTryingToShoot = false;
        arrow.gameObject.SetActive(false);
        gaugeHeight = 1;
        gaugeLevel.rectTransform.sizeDelta = new Vector2(gaugeLevel.rectTransform.sizeDelta.x, gaugeHeight);
    }

    public void SetupArrowAndCamera()
    {
        currentGoal = GameObject.FindWithTag("Hole" + ball.targetHoleIndex).GetComponent<Transform>().position;
        dirToGoal = currentGoal - ballGO.transform.position;
        arrow.gameObject.SetActive(true);
        arrow.position = ballGO.transform.position + dirToGoal.normalized;
        Camera.main.transform.position = Vector3.up * 2 + arrow.position + dirToGoal.normalized * -6;
        arrow.position += clubs[currentClub];
        arrow.LookAt(ballGO.transform.position);
    }


    void Shoot()
    {
        ball.hits++;
        rb.constraints = RigidbodyConstraints.None;
        float strengthPower = gaugeHeight / maxGaugeHeight;
        Vector3 dir = arrow.transform.position - ballGO.transform.position;
        rb.AddForce(dir * clubStrength[currentClub] * strengthPower, ForceMode.Impulse);
        TurnOffShootingMode();
        StartCoroutine(SetNewCameraPos());
    }

    void ShotDirectionInput()
    {
        if (Input.GetKey(KeyCode.A))
        {

            Camera.main.transform.RotateAround(ballGO.transform.position, Vector3.up, 100 * Time.deltaTime);
            arrow.transform.RotateAround(ballGO.transform.position, Vector3.up, 100 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.RotateAround(ballGO.transform.position, Vector3.up, -100 * Time.deltaTime);
            arrow.transform.RotateAround(ballGO.transform.position, Vector3.up, -100 * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            arrow.position -= clubs[currentClub];
            currentClub = currentClub + 1 < clubs.Length ? currentClub + 1 : 0;
            arrow.position += clubs[currentClub];
            clubText.text = "Club:\n" + clubName[currentClub];
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            arrow.position -= clubs[currentClub];
            currentClub = currentClub > 0 ? currentClub - 1 : clubs.Length - 1;
            arrow.position += clubs[currentClub];
            clubText.text = "Club:\n" + clubName[currentClub];
        }
    }

    void Update()
    {
        if (!GameManager.gm.isInFreeView)
        {
            if (arrow.gameObject.activeSelf)
                ShotDirectionInput();
            if (!isTryingToShoot)
            {
                if (Input.GetKeyDown(KeyCode.Space) && rb.constraints == RigidbodyConstraints.FreezeAll)
                {
                    isTryingToShoot = true;
                    clubText.text = "Club:\n" + clubName[currentClub];
                }
            }
            else
            {
                DetermineShotStrength();
                if (Input.GetKeyDown(KeyCode.Space) && rb.constraints == RigidbodyConstraints.FreezeAll)
                    Shoot();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                TurnOffShootingMode();
                GameManager.gm.isInFreeView = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.gm.isInFreeView = false;
                SetupArrowAndCamera();
            }
        }
    }

    IEnumerator SetNewCameraPos()
    {
        yield return new WaitForSeconds(1);
        while (rb.velocity.magnitude > 0.8f)
        {
            yield return new WaitForSeconds(1);
        }
        SetupArrowAndCamera();
    }
}
