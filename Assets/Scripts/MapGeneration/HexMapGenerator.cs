using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMapGenerator : MonoBehaviour {

    public int mapSize;
    public bool flatTestMap = false;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;
    public float mapToHexFactor;
    public Vector2 offset;

    public bool randomSeed;
    public int mapSeed;
    public int tempSeed;
    public int humidSeed;

    public bool pangea;
    public bool generateBiomes;

    public HexGrid hexGrid;
    public HexTerrainType[] regions;
    public HexBiomes[] biomes;

    public void GenerateMap() {
        if (flatTestMap) {
            hexGrid.CreateMap(mapSize);
        }
        else {
            float[,] noiseMap;
            float[,] tempMap;
            float[,] humidMap;

            if (randomSeed) {
                mapSeed = Random.Range(int.MinValue, int.MaxValue - 2);
                tempSeed = mapSeed + 1;
                humidSeed = mapSeed + 2;
            }

            noiseMap = Noise.GenerateNoiseMap(mapSize, mapSize, mapSeed, noiseScale, octaves, persistance, lacunarity, offset);

            hexGrid.CreateMap(noiseMap, mapToHexFactor, pangea);

            if (generateBiomes) {
                tempMap = Noise.GenerateNoiseMap(mapSize, mapSize, tempSeed, noiseScale, octaves, persistance, lacunarity, offset);
                humidMap = Noise.GenerateNoiseMap(mapSize, mapSize, humidSeed, noiseScale, octaves, persistance, lacunarity, offset);

                hexGrid.AddBiomes(tempMap, humidMap);
            }
        }    
    }

    private void OnValidate() {
        if (mapSize < 1) {
            mapSize = 1;
        }
        if (lacunarity < 1) {
            lacunarity = 1;
        }
        if (octaves < 0) {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct HexTerrainType {
    public string name;
    public float height;
    public TerrainType terrain;
}

[System.Serializable]
public struct HexBiomes {
    public string name;
    public Vector2 heightRange;
    public Vector2 tempRange;
    public Vector2 moistureRange;
    public BiomeType biome;
}
