using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    public GameObject normalTile;
    public GameObject mineTile;

    public GameObject GetType(GrassType type) {

        if (type == GrassType.normal) {
            return normalTile;
        }
        else if (type == GrassType.mine) {
            return mineTile;
        }

        return null;
    }

    public enum GrassType {
        normal, mine
    }
}
