using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TerrainGenerator terrainGenerator = (TerrainGenerator)target;

        if (DrawDefaultInspector())
        {
            if (terrainGenerator.AutoUpdate)
            {
                Generate(terrainGenerator);
            }
        }

        if (GUILayout.Button("Generate Terrain"))
        {
            Generate(terrainGenerator);
        }
        if (GUILayout.Button("DeleteAll"))
        {
            terrainGenerator.DeleteAll();
        }
    }

    void Generate(TerrainGenerator terrainGenerator)
    {
        terrainGenerator.DeleteAll();
        terrainGenerator.GenerateAll();
    }


}
