using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    // Start is called before the first frame update
    public bool isSelected = false;

    SpriteRenderer playerSprite;

    public override void Start()
    {
        base.Start();
        playerSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        opponentTag = "Ennemy";
    }

    void Update()
    {
        if (isSelected)
            playerSprite.color = new Color(1, 1, 1);
        else
            playerSprite.color = new Color(0.65f, 0.65f, 0.65f);
        if (!dying)
            UnitBehaviorLoop();
    }
}
