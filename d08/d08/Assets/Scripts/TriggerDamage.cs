using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    Character chara;
    // Start is called before the first frame update
    void Start()
    {
        chara = GetComponentInParent<Character>();
        
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
