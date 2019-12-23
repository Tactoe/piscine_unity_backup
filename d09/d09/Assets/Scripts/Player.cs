using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Gun[] guns;
    Gun gun;
    public int maxHealth;
    public int health;
    public bool isDead;

    void GameOver()
    {
        GameManager.gm.ReloadScene();
    }
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        gun = guns[0].GetComponent<Gun>();
    }

    void SwitchGun(int newGun)
    {
        gun.gameObject.SetActive(false);
        gun = guns[newGun];
        gun.gameObject.SetActive(true);
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchGun(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchGun(1);
        if (gun.canShoot && Input.GetMouseButtonDown(0))
            gun.Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (transform.position.y < -20 || transform.position.y > 0)
                health = 0;
            HandleInput();
            if (health <= 0)
            {
                GetComponent<FirstPersonController>().enabled = false;
                isDead = true;
                Invoke("GameOver", 5);
            }
        }
    }
}
