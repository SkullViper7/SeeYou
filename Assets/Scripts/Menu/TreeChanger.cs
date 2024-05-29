using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChanger : MonoBehaviour
{
    TreePrototype[] cache;
    TreePrototype[] treeType;
    [SerializeField] GameObject _Grass;
    [SerializeField] GameObject _Corn;

    void Start()
    {
        cache = Terrain.activeTerrain.terrainData.treePrototypes;
        treeType = Terrain.activeTerrain.terrainData.treePrototypes;

        treeType[0].prefab = _Grass;
        treeType[1].prefab = _Corn;
        Terrain.activeTerrain.terrainData.treePrototypes = treeType;
    }
}
