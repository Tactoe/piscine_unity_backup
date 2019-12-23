using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;
    public GameObject weaponGO;
    public Weapon weapon;
    public AudioSource pickupAudio;
    public AudioSource deathAudio;

    protected Transform target;
    protected Transform[] body;
    protected Animator animator;
    // Start is called before the first frame update
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        if (weaponGO != null)
            EquipWeapon();
        body = transform.GetComponentsInChildren<Transform>();    }

    public void TryPickingUp()
    {
        Collider2D pickupCollider = Physics2D.OverlapCircle(transform.position, 2, LayerMask.GetMask("Pickup"));
        if (pickupCollider)
        {
            if (weaponGO != null)
            {
                weaponGO.transform.position = pickupCollider.transform.position;
                UnequipWeapon();
            }
            weaponGO = pickupCollider.gameObject;
            EquipWeapon();
        }
        // if raycast to check if no walls
    }

    void EquipWeapon()
    {
        if (gameObject.tag == "Player")
            pickupAudio.Play();
        weaponGO.layer = 0;
        weapon = weaponGO.GetComponent<Weapon>();
        weapon.user = gameObject;
        weapon.dealsDamageTo = new List<string> { gameObject.CompareTag("Player") ? "Ennemy" : "Player" };
        weapon.target = target;
    }

    public void UnequipWeapon()
    {
        if (weaponGO != null)
        {
            weapon.user = null;
            weaponGO.layer = LayerMask.NameToLayer("Pickup");
            weaponGO = null;
            weapon = null;
        }
    }

    public virtual void Die()
    {
        gameObject.tag = "Untagged";
        //foreach(SpriteRenderer spr in gameObject.GetComponentsInChildren<SpriteRenderer>())
            //spr.sortingLayerName = "map";
        UnequipWeapon();
        animator.SetTrigger("die");
        deathAudio.Play();
        Destroy(gameObject.GetComponent<Collider2D>());
    }

    void LookAtTarget()
    {
        Vector3 direction = (Vector2)target.position - Vector2.one * transform.position;
        direction.Normalize();

        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        body[1].rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
        body[2].rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
        body[3].rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
        float dist = 0.7f;
        if (weaponGO != null)
        {
            weaponGO.transform.position = new Vector3(transform.position.x + direction.x * dist, transform.position.y + direction.y * dist, transform.position.z);
            weaponGO.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
    }


    // Update is called once per frame
    public virtual void Update()
    {
        LookAtTarget();
    }

}        