using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Balloon : MonoBehaviour {

	public float deflationStrength;
    public float staminaRegen;
    public float maxStamina;
    public float maxScale;
    public Text staminaText;

	private Vector3 inflation;
	private float stamina;
	private float deflation;
    private float timer;

    void Start () {
		stamina = maxStamina;
        timer = 0;
		inflation = new Vector3(1, 1, 1);
	}
	
	void Update () {
        timer += Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.Space) && stamina > 0)
		{
			inflation += new Vector3 (0.5f, 0.5f, 0.5f);
			stamina -= 0.5f;
		}
        else if (stamina <= maxStamina)
            stamina += staminaRegen * Time.deltaTime;
		deflation = deflationStrength * Time.deltaTime;
		inflation -= new Vector3 (deflation, deflation, deflation);
		this.gameObject.transform.localScale = inflation;
        staminaText.text = "Stamina: " + Mathf.RoundToInt(stamina);
        if (this.gameObject.transform.localScale.x < 0
            || this.gameObject.transform.localScale.x > maxScale)
		{
            Destroy(this.gameObject);
            Debug.Log("Balloon life time: " + Mathf.RoundToInt(timer) + "s");
		}
	}
}
