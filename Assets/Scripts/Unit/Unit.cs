using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Unit : MonoBehaviour 
{
    bool isSelected;
    public GameObject selectedIcon;

    public GameObject customGUI;
    public HexCell currentHexCell;

    public float moveSpeed;
    public float rotationSpeed;
    Rigidbody rb;
    float vertical = 0f;
    float horizontal = 0f;
    float sqrt2;

    public TextMeshProUGUI unitName;

    void Start() {
        sqrt2 = Mathf.Sqrt(2);
        isSelected = false;
        selectedIcon.SetActive(false);
        customGUI.SetActive(false);

        rb = GetComponent<Rigidbody>();
    }

    public void SelectUnit() {
        isSelected = true;
        selectedIcon.SetActive(true);
        customGUI.SetActive(true);
    }

    public void DeselectUnit() {
        isSelected = false;
        selectedIcon.SetActive(false);
        customGUI.SetActive(false);
    }

    public void EditName(string newName) {
        unitName.text = newName;
    }

    public void DeleteUnit() {
        DeselectUnit();
        GameObject.Find("InputManager").GetComponent<InputManager>().DeselectEverything();
        Destroy(this.gameObject);
    }

    public bool SettleCity() {
        if (currentHexCell.canBuildCity) {
            currentHexCell.BuildCityCenter();
            currentHexCell.cityModel.GetComponent<CityManager>().CityCenter = currentHexCell;
            return true;
        }
        return false;
    }

    void Update() {
        if (isSelected) {
            if (currentHexCell != null && !currentHexCell.isSelected) {
                currentHexCell.SelectCell();
            }                

            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");   
        }
        else {
            if (currentHexCell != null && currentHexCell.isSelected) {
                currentHexCell.DeselectCell();
            }
        }
    }

    void FixedUpdate() {
        if (isSelected) {

            //Debug.Log("horz: " + horizontal + ", vert: " + vertical);
            // move unit
            Vector3 velocity = new Vector3(horizontal, 0f, vertical) * Time.deltaTime * moveSpeed;

            if (horizontal > 0 || horizontal < 0 && vertical > 0 || vertical < 0) {
                velocity /= sqrt2;
            }

            rb.velocity = velocity;


            // rotate unit toward direction of movemet
            Vector3 rotationVector = new Vector3(horizontal, 0f, vertical);
            Vector3 newRotation = Vector3.RotateTowards(transform.forward, rotationVector, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newRotation);
        }
        else {

        }
    }
}
