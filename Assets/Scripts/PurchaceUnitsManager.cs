using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaceUnitsManager : MonoBehaviour
{
    public CityManager city;
    public WorldUnitManager unitManager;
    HexDirection direction;

    public GameObject settlerPrefab;
    public GameObject builderPrefab;
    public GameObject defensivePrefab;

    void Awake() {
        unitManager = GameObject.Find("WorldUnits").GetComponent<WorldUnitManager>();
    }

    private Vector3 GetSpawnPos() {
        CycleDirection();
        return city.CityCenter.GetNeighbor(direction).transform.position;
    }

    private void CycleDirection() {
        if (direction == HexDirection.E) {
            direction = HexDirection.NE;
        }
        else if (direction == HexDirection.NE) {
            direction = HexDirection.NW;
        }
        else if (direction == HexDirection.NW) {
            direction = HexDirection.W;
        }
        else if (direction == HexDirection.W) {
            direction = HexDirection.SW;
        }
        else if (direction == HexDirection.SW) {
            direction = HexDirection.SE;
        }
        else if (direction == HexDirection.SE) {
            direction = HexDirection.E;
        }
    }

    public void SpawnSettler() {
        GameObject settler = Instantiate(settlerPrefab, unitManager.transform);
        unitManager.units.Add(settler.GetComponent<Unit>());
        settler.transform.position = GetSpawnPos();
    }

    public void SpawnBuilder() {
        GameObject builder = Instantiate(builderPrefab, unitManager.transform);
        unitManager.units.Add(builder.GetComponent<Unit>());
        builder.transform.position = GetSpawnPos();
    }

    public void SpawnDefensiveUnit() {
        GameObject unit = Instantiate(defensivePrefab, unitManager.transform);
        unitManager.units.Add(unit.GetComponent<Unit>());
        unit.transform.position = GetSpawnPos();
    }
}
