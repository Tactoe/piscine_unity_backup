using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    Ennemy chara;
    // Start is called before the first frame update
    void Start()
    {
        chara = GetComponentInParent<Ennemy>();
        
    }

    public void TriggerDamageFunc()
    {
        chara.DealDamageToTarget();
    }

    public void TriggerDeathFunc()
    {
        chara.StartDeathCoroutine();
    }
}
