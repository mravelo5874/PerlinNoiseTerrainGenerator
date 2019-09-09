using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainManager : MonoBehaviour {
    public GameObject normalTile;
    public GameObject volcanoTile;

    public GameObject GetType(MountainType type) {

        if (type == MountainType.normal) {
            return normalTile;
        }
        else if (type == MountainType.volcano) {
            return volcanoTile;
        }

        return null;
    }

    public enum MountainType {
        normal, volcano
    }
}
