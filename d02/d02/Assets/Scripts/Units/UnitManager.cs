using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<PlayerUnit> playerUnits;

    List<PlayerUnit> selectedUnits = new List<PlayerUnit>();
    Vector3 target;

    void SelectUnit(PlayerUnit unit)
    {
        unit.isSelected = true;
        selectedUnits.Add(unit);
    }

    void ClearSelectedUnits()
    {
        foreach (PlayerUnit unit in selectedUnits)
            unit.isSelected = false;
        selectedUnits.Clear();
    }

    void Start()
    {
        target = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
            ClearSelectedUnits();
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            RaycastHit2D hit = Physics2D.Raycast(target, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Shootable"));
            if (hit.collider)
            {
                if (hit.collider.CompareTag("Player") && hit.collider.GetComponent<PlayerUnit>() != null)
                {
                    if (Input.GetKey(KeyCode.LeftControl) || selectedUnits.Count == 0)
                        SelectUnit(hit.collider.GetComponent<PlayerUnit>());
                    else
                    {
                        ClearSelectedUnits();
                        SelectUnit(hit.collider.GetComponent<PlayerUnit>());
                    }
                }
            }
            else
            {
                foreach (PlayerUnit unit in selectedUnits)
                    if (unit != null)
                        unit.SetNewTargetDirection(target);
            }
        }
    }
}
