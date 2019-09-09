using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    HexCell[] cells;
    public GameObject cellPrefab;
    public int cellCountZ, cellCountX;

    public Transform hexGrid;
    private float[,] noiseMap;

    public TerrainManager terrainManager;
    public FeatureManager featureManager;
    public HexMapGenerator mapGenerator;
    

    void Awake() {
        mapGenerator.GenerateMap();
    }

    public void CreateMap(int size) {
        noiseMap = null;
        cellCountX = size;
        cellCountZ = size;
        cells = new HexCell[cellCountX * cellCountZ];

        for (int z = 0, i = 0; z < cellCountZ; z++) {
            for (int x = 0; x < cellCountX; x++) {
                CreateCell(x, z, i++);
            }
        }
    }

    public void CreateMap(float[,] _noiseMap, float mapToHexFactor, bool isPangea) {
        int width = _noiseMap.GetLength(0);
        int height = _noiseMap.GetLength(1);

        if (isPangea) {
            float[,] pangeaMap = FalloffGenerator.GenerateFalloffMap(width);
            PangeaMapData(_noiseMap, pangeaMap, width);
        }
        else {
            noiseMap = _noiseMap;
        }
        
        cellCountX = Mathf.FloorToInt(width * mapToHexFactor);
        cellCountZ = Mathf.FloorToInt(height * mapToHexFactor);

        cells = new HexCell[cellCountX * cellCountZ];

        for (int z = 0, i = 0; z < cellCountZ; z++) {
            for (int x = 0; x < cellCountX; x++) {
                CreateCell(x, z, i++);
            }
        }
    }

    void CreateCell(int x, int z, int i) {
        float halfWidth = (cellCountX * HexMetrics.innerRadius * 2f) / 2;
        float halfHeight = (cellCountZ * HexMetrics.outerRadius* 1.5f) / 2;

        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f) - halfWidth;
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f) - halfHeight;

        GameObject cell = Instantiate(cellPrefab, hexGrid);
        HexCell hexCell = cell.GetComponent<HexCell>();
        cell.transform.localPosition = position;
        cells[i] = hexCell;

        if (x > 0) {
            hexCell.SetNeighbor(HexDirection.W, cells[i - 1]);
        }
        if (z > 0) {
            if ((z & 1) == 0) {
                hexCell.SetNeighbor(HexDirection.SE, cells[i - cellCountX]);
                if (x > 0) {
                    hexCell.SetNeighbor(HexDirection.SW, cells[i - cellCountX - 1]);
                }
            }
            else {
                hexCell.SetNeighbor(HexDirection.SW, cells[i - cellCountX]);
                if (x < cellCountX - 1) {
                    hexCell.SetNeighbor(HexDirection.SE, cells[i - cellCountX + 1]);
                }
            }
        }

        if (noiseMap != null) {
            float height = noiseMap[(int)(x / mapGenerator.mapToHexFactor), (int)(z / mapGenerator.mapToHexFactor)];
            for (int j = 0; j < mapGenerator.regions.Length; j++) {
                if (height <= mapGenerator.regions[j].height) {
                    hexCell.terrainType = mapGenerator.regions[j].terrain;
                    hexCell.SetHexTerrain(terrainManager.GetTerrain(hexCell.terrainType));
                    break;
                }
            }
        }
        else {
            Debug.Log("yeet");
            hexCell.terrainType = TerrainType.Grass;
            hexCell.SetHexTerrain(terrainManager.GetTerrain(hexCell.terrainType));
        }    
        
        if (hexCell.terrainType == TerrainType.Ocean) {
            hexCell.MakeCellInpassable();
        }
    }

    public void AddBiomes(float[,] tempMap, float[,] humidMap) {
        for (int z = 0; z < cellCountZ; z++) {
            for (int x = 0; x < cellCountX; x++) {
                float temp = tempMap[(int)(x / mapGenerator.mapToHexFactor), (int)(z / mapGenerator.mapToHexFactor)];
                float moisture = humidMap[(int)(x / mapGenerator.mapToHexFactor), (int)(z / mapGenerator.mapToHexFactor)];
                float height = noiseMap[(int)(x / mapGenerator.mapToHexFactor), (int)(z / mapGenerator.mapToHexFactor)];

                for (int i = 0; i < mapGenerator.biomes.Length; i++) {
                    
                    if (temp >= mapGenerator.biomes[i].tempRange.x &&
                        temp <= mapGenerator.biomes[i].tempRange.y) {

                        if (moisture >= mapGenerator.biomes[i].moistureRange.x &&
                            moisture <= mapGenerator.biomes[i].moistureRange.y) {

                            if (height >= mapGenerator.biomes[i].heightRange.x &&
                                height <= mapGenerator.biomes[i].heightRange.y) {

                                cells[(cellCountZ * z) + x].biomeType = mapGenerator.biomes[i].biome;
                                featureManager.AddBiomeFeatures(cells[(cellCountZ * z) + x], mapGenerator.biomes[i].biome);
                                break;
                            }
                        }
                    }
                }

                cells[(cellCountZ * z) + x].biomeType = BiomeType.None;
            }
        }
    }

    void PangeaMapData(float[,] _noiseMap, float[,] pangeaMap, int size) {
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                _noiseMap[x, y] = Mathf.Clamp01(_noiseMap[x, y] - pangeaMap[x, y]);
            }
        }

        noiseMap = _noiseMap;
    }

    public void DeselectAllCells() {
        foreach (HexCell cell in cells) {
            cell.DeselectCell();
        }
    }
}
