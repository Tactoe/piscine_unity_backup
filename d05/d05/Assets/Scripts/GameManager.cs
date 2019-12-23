using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gm;
    public bool isInFreeView;
    public bool finished;
    private void Awake()
    {
        if (gm == null)
            gm = this;
    }
}
