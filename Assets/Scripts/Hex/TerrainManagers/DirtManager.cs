using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtManager : MonoBehaviour {
    public GameObject normalTile;
    public GameObject mineTile;

    public GameObject GetType(DirtType type) {

        if (type == DirtType.normal) {
            return normalTile;
        }
        else if (type == DirtType.mine) {
            return mineTile;
        }

        return null;
    }

    public enum DirtType {
        normal, mine
    }
}
