using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{

    [HideInInspector] public int health;
    [HideInInspector] public bool dying;
    public int maxHealth;
    public AudioClip deathSound;
    public Spawner spawner;

    public virtual void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !dying)
        {
            dying = true;
            spawner.spawnDelay += 2.5f;
            SoundManager.instance.PlaySound(deathSound);
            Destroy(gameObject);
        }
    }
}
