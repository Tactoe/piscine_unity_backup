using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : Tank
{
    // Start is called before the first frame update
    public Text stats;

    public override void Start()
    {
        base.Start();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void HandleInput()
    {
        float xAxis = rotationSpeed * 2 * Input.GetAxis("Horizontal") * Time.deltaTime;
        float yAxis = speed * Input.GetAxis("Vertical") * Time.deltaTime;
        float mouse = rotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;

        tankTop.Rotate(Vector3.up * mouse);
        Camera.main.transform.RotateAround(transform.position, Vector3.up, mouse);
        tankBottom.Rotate(Vector3.up * xAxis);
        transform.Translate(tankBottom.forward * yAxis);

        if (Input.GetKeyDown(KeyCode.LeftShift) && canUseBoost)
            StartCoroutine(Boost());
        if (Input.GetMouseButtonDown(0))
            Shoot(false);
        if (Input.GetMouseButtonDown(1) && missileAmmo > 0)
        {
            missileAmmo--;
            Shoot(true);
        }
    }

    public override void Die()
    {
        Camera.main.transform.SetParent(null);
        Instantiate(missileExplosion, transform.position, Quaternion.identity);
        Destroy(tankTop.gameObject);
        Destroy(tankBottom.gameObject);
        GameManager.gm.GameOver();
        Destroy(this);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (gameObject != null)
        {
            stats.text = "HP: " + health + "\nMissiles: " + missileAmmo;
            HandleInput();
        }
    }
}
