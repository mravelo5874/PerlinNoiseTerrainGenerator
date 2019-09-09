using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexWallManager : MonoBehaviour
{
    public GameObject wall_ne;
    public GameObject wall_nw;
    public GameObject wall_e;
    public GameObject wall_se;
    public GameObject wall_sw;
    public GameObject wall_w;

    public HexCell hexCell;

    public void UpdateWalls() {
        if (hexCell.isWalled) {
            // east wall
            if (hexCell.GetNeighbor(HexDirection.E).isWalled) {
                wall_e.SetActive(false);
            }
            else { wall_e.SetActive(true); }

            // north east wall
            if (hexCell.GetNeighbor(HexDirection.NE).isWalled) {
                wall_ne.SetActive(false);
            }
            else { wall_ne.SetActive(true); }

            // north west wall
            if (hexCell.GetNeighbor(HexDirection.NW).isWalled) {
                wall_nw.SetActive(false);
            }
            else { wall_nw.SetActive(true); }

            // south east wall
            if (hexCell.GetNeighbor(HexDirection.SE).isWalled) {
                wall_se.SetActive(false);
            }
            else { wall_se.SetActive(true); }

            // south west wall
            if (hexCell.GetNeighbor(HexDirection.SW).isWalled) {
                wall_sw.SetActive(false);
            }
            else { wall_sw.SetActive(true); }

            // west wall
            if (hexCell.GetNeighbor(HexDirection.W).isWalled) {
                wall_w.SetActive(false);
            }
            else { wall_w.SetActive(true); }
        }
    }
}
