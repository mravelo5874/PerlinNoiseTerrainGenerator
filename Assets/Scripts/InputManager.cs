using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public bool interactableTiles;
    public bool interactableUnits;

    public HexGrid hexGrid;
    public WorldUnitManager unitManager;
    public CivilizationManager civManager;

    public HexMapCamera hexCamera;
    public HexCell currentSelectedCell;
    public CityManager currentSelectedCity;
    public Unit currentSelectedUnit;

    public float rayLength;
    public LayerMask layerMask;
    public GameObject terrainPanel;

    private bool expandBordersMode = false;
    public CityManager cityExpanding;

    void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast (ray, out hit, rayLength, layerMask)) {
                Debug.Log("Selected: " + hit.transform.name);

                if (expandBordersMode) {
                    HexCell hex = hit.transform.GetComponent<HexCell>();
                    if (hex.isHighlighted) { cityExpanding.AddCellToCityBorders(hex); }
                }
                else {
                    // tiles are interactable
                    if (interactableTiles) {
                        HexCell hex = hit.transform.GetComponent<HexCell>();
                        if (hex != null) {
                            DeselectEverything();

                            currentSelectedCell = hex;
                            terrainPanel.SetActive(true);
                            currentSelectedCell.SelectCell();
                        }
                        else {
                            terrainPanel.SetActive(false);
                            if (currentSelectedCell != null) {
                                DeselectEverything();
                            }
                        }
                    }

                    // select city
                    CityManager city = hit.transform.GetComponent<CityManager>();
                    if (city != null) {
                        DeselectEverything();

                        currentSelectedCity = city;
                        hexCamera.SetGameObjectToFollow(city.gameObject);
                        currentSelectedCity.SelectCity();
                    }
                    else {
                        if (currentSelectedCity != null) {
                            DeselectEverything();
                        }
                    }

                    // units are interactable
                    if (interactableUnits) {
                        Unit unit = hit.transform.GetComponentInParent<Unit>();
                        if (unit != null) {
                            DeselectEverything();

                            currentSelectedUnit = unit;
                            hexCamera.SetGameObjectToFollow(unit.gameObject);
                            currentSelectedUnit.SelectUnit();
                        }
                        else {
                            if (currentSelectedUnit != null) {
                                DeselectEverything();
                            }
                        }
                    }
                }
            }
            else {
                DeselectEverything();
            }
        }
    }

    public void SetBorderExpandMode(CityManager _city) {
        cityExpanding = _city;
        expandBordersMode = true;
    }

    public void EndBorderExpandMode() {
        cityExpanding = null;
        expandBordersMode = false;
    }

    public void DeselectEverything() {

        hexGrid.DeselectAllCells();
        unitManager.DeselectAllUnits();

        terrainPanel.SetActive(false);
        if (currentSelectedCell != null) {
            currentSelectedCell.DeselectCell();
            currentSelectedCell = null;
        }

        if (currentSelectedCity != null) {
            currentSelectedCity.DeselectCity();
            currentSelectedCity = null;
        }


        if (currentSelectedUnit != null) {
            currentSelectedUnit.DeselectUnit();
            currentSelectedUnit = null;
        }

        civManager.UnselectAllCities();
        hexCamera.UnsetGameObjectToFollow();
    }
}
