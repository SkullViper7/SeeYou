using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChanger : MonoBehaviour
{
    TreePrototype[] cache;
    TreePrototype[] treeType;
    [SerializeField] GameObject _Grass;
    [SerializeField] GameObject _Corn;

    [SerializeField] Terrain _terrain;

    void Start()
    {
        cache = _terrain.terrainData.treePrototypes;
        treeType = _terrain.terrainData.treePrototypes;

        treeType[0].prefab = _Grass;
        treeType[1].prefab = _Corn;
        Terrain.activeTerrain.terrainData.treePrototypes = treeType;
    }
}
