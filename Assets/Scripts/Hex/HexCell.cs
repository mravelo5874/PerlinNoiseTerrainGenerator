using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HexCell : MonoBehaviour
{
    public TerrainType terrainType;
    public BiomeType biomeType;
    public GameObject terrain;
    public GameObject feature;

    public GameObject HexHighlighted;
    public GameObject SphereCollider;
    public Animator SelectedAnim;
    public bool isSelected = false;
    public bool isHighlighted = false;
    public bool canBePassedThrough = true;

    [SerializeField]
    HexCell[] neighbors;

    public GameObject cityModel;
    public bool canBuildCity = true;
    public bool isCityCenter = false;
    public bool isWalled = false;

    void Start() {
        DeselectCell();
        cityModel.SetActive(false);
        HexHighlighted.SetActive(false);
    }

    public HexCell GetNeighbor(HexDirection direction) {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(HexDirection direction, HexCell cell) {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }

    public void SelectCell() {
        isSelected = true;
        SelectedAnim.SetBool("isSelected", true);
    }

    public void DeselectCell() {
        isSelected = false;
        SelectedAnim.SetBool("isSelected", false);
    }

    public void HighlightCell() {
        HexHighlighted.SetActive(true);
        isHighlighted = true;
    }

    public void UnhighlightCell() {
        HexHighlighted.SetActive(false);
        isHighlighted = false;
    }

    public void BuildCityCenter() {
        if (canBuildCity) {
            isCityCenter = true;
            isWalled = true;
            GetComponent<HexWallManager>().UpdateWalls();
            cityModel.SetActive(true);
            cityModel.GetComponent<CityManager>().SelectCity();
            GameObject.Find("CivManager").GetComponent<CivilizationManager>().AddCityToCiv(cityModel.GetComponent<CityManager>());
        }
    }

    public void SetHexTerrain(GameObject tile) {
        if (tile != null) {

            // remove all children
            foreach (Transform child in terrain.transform) {
                GameObject.Destroy(child.gameObject);
            }

            Instantiate(tile, terrain.transform);
        }      
    }

    public void SetHexFeature(GameObject _feature) {
        if (_feature != null) {

            // remove all children
            foreach (Transform child in feature.transform) {
                GameObject.Destroy(child.gameObject);
            }

            Instantiate(_feature, feature.transform);
        }
    }

    public void MakeCellInpassable() {
        canBePassedThrough = false;
        SphereCollider.SetActive(true);
    }

    public void MakeCellPassable() {
        canBePassedThrough = true;
        SphereCollider.SetActive(false);
    }
}
