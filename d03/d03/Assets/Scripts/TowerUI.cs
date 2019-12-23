using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerUI : MonoBehaviour, IDropHandler, IDragHandler
{

    public GameObject towerObject;
    public GameObject towerDataTextObject;

    public bool canBePurchased;
    Image spr;
    towerScript tower;
    Text damageTxt;
    Text costTxt;
    Text fireRateTxt;
    Text rangeTxt;
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        Text[] gameTexts = towerDataTextObject.GetComponentsInChildren<Text>();
        foreach (Text text in gameTexts)
        {
            if (text.gameObject.name == "Attack")
                damageTxt = text;
            else if (text.gameObject.name == "Cost")
                costTxt = text;
            else if(text.gameObject.name == "FireRate")
                fireRateTxt = text;
            else if (text.gameObject.name == "Range")
                rangeTxt = text;
        }
        tower = towerObject.GetComponent<towerScript>();
        spr = gameObject.GetComponent<Image>();
        initialPosition = transform.position;
        damageTxt.text = tower.damage.ToString();
        costTxt.text = tower.energy.ToString();
        fireRateTxt.text = tower.fireRate.ToString();
        rangeTxt.text = tower.range.ToString();
    }

    public void TryToPlaceTower()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;
        RaycastHit2D hit = Physics2D.Raycast(target, Vector2.zero, Mathf.Infinity);
        if (hit.collider)
        {
            if (hit.collider.CompareTag("empty"))
            {
                GameObject tmp = Instantiate(towerObject);
                tmp.transform.position = hit.collider.transform.position;
                hit.collider.tag = "tower";
                gameManager.gm.playerEnergy -= tower.energy;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (canBePurchased)
            TryToPlaceTower();
        transform.position = initialPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canBePurchased)
        {
            transform.position = Input.mousePosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        canBePurchased = gameManager.gm.playerEnergy >= tower.energy;
        if (!canBePurchased)
            spr.color = new Color(1f, 0.5f, 0.5f);
        else
            spr.color = new Color(1, 1, 1);
    }
}
