using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public GrassManager grass;
    public SandManager sand;
    public DirtManager dirt;
    public OceanManager ocean;
    public MountainManager mountain;
    [Range(0, 1)]
    public float volcanoPercent;
    

    public GameObject GetTerrain(TerrainType type) {
        if (type == TerrainType.Grass) {
            return grass.GetType(GrassManager.GrassType.normal);
        }
        if (type == TerrainType.Sand) {
            return sand.GetType(SandManager.SandType.normal);
        }
        if (type == TerrainType.Mountain) {
            float num = Random.Range(0f, 1f);
            if (num <= volcanoPercent) {
                return mountain.GetType(MountainManager.MountainType.volcano);
            }
            else {
                return mountain.GetType(MountainManager.MountainType.normal);
            }
        }
        if (type == TerrainType.Ocean) {
            return ocean.GetType(OceanManager.OceanType.normal);
        }
        if (type == TerrainType.Dirt) {
            return dirt.GetType(DirtManager.DirtType.normal);
        }

        return null;
    }

    public GameObject GetOasis() {
        return sand.GetType(SandManager.SandType.oasis);
    }
}
