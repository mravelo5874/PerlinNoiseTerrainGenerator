using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollision : MonoBehaviour {
    public Unit unit;

    void OnTriggerEnter(Collider other) {

        HexCell hex = other.transform.GetComponent<HexCell>();
        if (hex != null) {
            if (unit.currentHexCell != null) {
                unit.currentHexCell.DeselectCell();
            }
            unit.currentHexCell = hex;
        }
    }
}
