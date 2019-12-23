using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    Text victoryScore;
    Text rankScore;
    string[] ranks;
    // Start is called before the first frame update
    void Awake()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        victoryScore = texts[0];
        rankScore = texts[1];
        ranks = new string[] { "F", "E", "D", "C", "B", "A", "S", "SSS" };
    }

    string ComputeRank()
    {
        int rankPosition = ranks.Length - 2;
        int mHp = gameManager.gm.playerMaxHp;
        int hp = gameManager.gm.playerHp;
        int mNrg = gameManager.gm.playerStartEnergy;
        int nrg = gameManager.gm.playerEnergy;
        rankPosition -= Mathf.RoundToInt(gameManager.gm.playerMaxHp * 0.1f);
        int hpLoss = 0;
        while (hp + ((mHp / 4) + 1) * hpLoss < mHp)
            hpLoss++;
        rankPosition -= hpLoss;
        if (nrg >= mNrg)
        {
            int energyBoost = 1;
            energyBoost += nrg > mNrg * 2 ? 1 : 0;
            energyBoost += nrg > mNrg * 3 ? 1 : 0;
            rankPosition += energyBoost;
        }
        while (nrg < mNrg)
        {
            nrg += mNrg / 4;
            rankPosition--;
        }
        rankPosition = Mathf.Clamp(rankPosition, 0, ranks.Length);
        return ranks[rankPosition];
    }

    void OnEnable()
    {
        victoryScore.text = "Score: " + gameManager.gm.score;
        rankScore.text = "Rank: " + ComputeRank();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
