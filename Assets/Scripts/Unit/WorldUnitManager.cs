using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUnitManager : MonoBehaviour
{
    public List<Unit> units;
    public GameObject unitPrefab;

    void Awake() {
        GameObject testUnit = Instantiate(unitPrefab, this.transform);
        units.Add(testUnit.GetComponent<Unit>());
    }

    public void DeselectAllUnits() {
        foreach(Unit unit in units) {
            if (unit != null)
                unit.DeselectUnit();
        }
    }
}
