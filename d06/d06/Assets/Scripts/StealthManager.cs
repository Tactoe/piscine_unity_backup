using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StealthManager : MonoBehaviour
{
    Player player;
    Image stealthGauge;
    public Image alarmFilter;
    float maxGaugeValue;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        stealthGauge = GetComponentInChildren<Image>();
        maxGaugeValue = stealthGauge.rectTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.ennemyDetectionAmount > 75)
        {
            Color Opaque = new Color(alarmFilter.color.r, alarmFilter.color.g, alarmFilter.color.b, 0.2f);
            alarmFilter.color = Color.Lerp(alarmFilter.color, Opaque, 20 * Time.deltaTime);
            stealthGauge.color = Color.red;
        }
        else
        {
            Color Transparent = new Color(alarmFilter.color.r, alarmFilter.color.g, alarmFilter.color.b, 0);
            alarmFilter.color = Color.Lerp(alarmFilter.color, Transparent, 5 * Time.deltaTime);
            stealthGauge.color = Color.gray;
        }
        stealthGauge.rectTransform.sizeDelta = new Vector2(player.ennemyDetectionAmount / 100 * maxGaugeValue ,stealthGauge.rectTransform.sizeDelta.y);
    }
}
