using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettlerManager : MonoBehaviour
{
    public Unit unit;
    public TextMeshProUGUI chargesText;
    public int chargesLeft;

    void Start() {
        chargesLeft = 1;
        unit.EditName("Settler");
    }

    public void SettleCity() {
        if (unit.SettleCity()) {
            unit.DeleteUnit();
        }
    }
}
