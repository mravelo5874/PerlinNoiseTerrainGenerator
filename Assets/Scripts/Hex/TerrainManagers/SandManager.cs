using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandManager : MonoBehaviour {
    public GameObject normalTile;
    public GameObject mineTile;
    public GameObject oasisTile;

    public GameObject GetType(SandType type) {

        if (type == SandType.normal) {
            return normalTile;
        }
        else if (type == SandType.mine) {
            return mineTile;
        }
        else if (type == SandType.oasis) {
            return oasisTile;
        }

        return null;
    }

    public enum SandType {
        normal, mine, oasis
    }
}
