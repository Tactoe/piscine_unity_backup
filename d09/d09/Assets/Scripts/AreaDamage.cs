using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ennemy"))
            other.gameObject.GetComponent<Ennemy>().GetHit(damage);
    }

}
