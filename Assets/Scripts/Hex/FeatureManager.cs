using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureManager : MonoBehaviour
{
    public GameObject sandDunes;
    [Range(0, 1)]
    public float sandDunePercent;
    [Range(0, 1)]
    public float oasisPersent;

    public GameObject decidiousForest;
    [Range(0, 1)]
    public float decidiousForestPercent;

    public GameObject fruitForest;
    [Range(0, 1)]
    public float fruitForestPercent;

    public GameObject pineForest;
    [Range(0, 1)]
    public float pineForestPercent;

    public GameObject hills;
    [Range(0, 1)]
    public float hillsPercent;

    public TerrainManager terrainManager;

    public void AddBiomeFeatures(HexCell cell, BiomeType biome) {
        if (cell != null) {

            float num = Random.Range(0f, 1f);
            //Debug.Log(num);
            if (biome == BiomeType.DecidiousForest) {
                if (num <= decidiousForestPercent) {
                    if (num <= fruitForestPercent) {
                        cell.SetHexFeature(fruitForest);
                    }
                    else {
                        cell.SetHexFeature(decidiousForest);
                    }                
                } 
            }
            else if (biome == BiomeType.PineForest) {
                if (num <= pineForestPercent) {
                    cell.SetHexFeature(pineForest);
                }
            }
            else if (biome == BiomeType.Desert) {
                if (num <= sandDunePercent) {
                    if (num <= oasisPersent) {
                        cell.terrainType = TerrainType.Sand;
                        cell.SetHexTerrain(terrainManager.GetOasis());
                    }
                    else {
                        cell.terrainType = TerrainType.Sand;
                        cell.SetHexTerrain(terrainManager.GetTerrain(cell.terrainType));
                        cell.SetHexFeature(sandDunes);
                    }
                }
            }
            else if (biome == BiomeType.hills){
                if (num <= hillsPercent) {
                    cell.SetHexFeature(hills);
                }
            }

        }
    }
}
