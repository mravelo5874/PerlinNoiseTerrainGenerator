using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour {
    public GameObject normalTile;

    public GameObject GetType(OceanType type) {

        if (type == OceanType.normal) {
            return normalTile;
        }

        return null;
    }

    public enum OceanType {
        normal
    }
}
