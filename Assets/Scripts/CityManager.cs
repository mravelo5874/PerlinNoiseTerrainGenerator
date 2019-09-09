using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    bool isSelected;
    bool expandBordersMode;
    bool purchaseUnitsMode;
    public GameObject selectedIcon;
    public GameObject customGUI;
    public GameObject purchaseUnitsGUI;

    public HexCell CityCenter;
    public List<HexCell> cityCells;
    public List<HexCell> tempCells;

    void Start() {
        isSelected = false;
        expandBordersMode = false;
        cityCells.Add(CityCenter);
        customGUI.SetActive(false);
        purchaseUnitsGUI.SetActive(false);
    }

    public void SelectCity() {
        isSelected = true;
        selectedIcon.SetActive(true);
        customGUI.SetActive(true);
    }

    public void DeselectCity() {
        isSelected = false;
        selectedIcon.SetActive(false);
        customGUI.SetActive(false);
        EndExpandBorders();
        ClosePurchaseUnitGUI();
    }

    public void ToggleExpandBorders() {
        if (!expandBordersMode) {
            HighlightPotentialCells();
            expandBordersMode = true;
        }
        else if (expandBordersMode) {
            EndExpandBorders();
        }
    }

    public void TogglePurchaseUnits() {
        if (!purchaseUnitsMode) {
            purchaseUnitsGUI.SetActive(true);
            purchaseUnitsMode = true;
        }
        else if (purchaseUnitsMode) {
            ClosePurchaseUnitGUI();
        }
    }

    private void ClosePurchaseUnitGUI() {
        purchaseUnitsGUI.SetActive(false);
        purchaseUnitsMode = false;
    }

    private void EndExpandBorders() {
        UnhighlightTempCells();
        tempCells.Clear();
        GameObject.Find("InputManager").GetComponent<InputManager>().EndBorderExpandMode();
        expandBordersMode = false;
    }


    public void UpdateCityWalls() {
        foreach (HexCell cell in cityCells) {
            cell.GetComponent<HexWallManager>().UpdateWalls();
        }
    }

    private void HighlightTempCells() {
        foreach (HexCell cell in tempCells) {
            cell.HighlightCell();
        }
    }

    private void UnhighlightTempCells() {
        foreach (HexCell cell in tempCells) {
            cell.UnhighlightCell();
        }
    }

    private void HighlightPotentialCells() {

        foreach(HexCell cell in cityCells) {
            if (!cell.GetNeighbor(HexDirection.E).isWalled && !cell.GetNeighbor(HexDirection.E).isHighlighted) {
                tempCells.Add(cell.GetNeighbor(HexDirection.E));
            }
            if (!cell.GetNeighbor(HexDirection.NE).isWalled && !cell.GetNeighbor(HexDirection.NE).isHighlighted) {
                tempCells.Add(cell.GetNeighbor(HexDirection.NE));
            }
            if (!cell.GetNeighbor(HexDirection.NW).isWalled && !cell.GetNeighbor(HexDirection.NW).isHighlighted) {
                tempCells.Add(cell.GetNeighbor(HexDirection.NW));
            }
            if (!cell.GetNeighbor(HexDirection.SE).isWalled && !cell.GetNeighbor(HexDirection.SE).isHighlighted) {
                tempCells.Add(cell.GetNeighbor(HexDirection.SE));
            }
            if (!cell.GetNeighbor(HexDirection.SW).isWalled && !cell.GetNeighbor(HexDirection.SW).isHighlighted) {
                tempCells.Add(cell.GetNeighbor(HexDirection.SW));
            }
            if (!cell.GetNeighbor(HexDirection.W).isWalled && !cell.GetNeighbor(HexDirection.W).isHighlighted) {
                tempCells.Add(cell.GetNeighbor(HexDirection.W));
            }
        }

        HighlightTempCells();

        GameObject.Find("InputManager").GetComponent<InputManager>().SetBorderExpandMode(this);
    }

    public void AddCellToCityBorders(HexCell newCell) {
        cityCells.Add(newCell);
        newCell.isWalled = true;
        UpdateCityWalls();
        EndExpandBorders();
    }
}
