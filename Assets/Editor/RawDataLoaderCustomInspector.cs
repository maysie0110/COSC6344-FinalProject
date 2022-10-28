
using UnityEngine;
using UnityEditor;

using System.IO;

[CustomEditor(typeof(RawDataLoader))]
public class RawDataLoaderCustomInspector : Editor
{
   
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RawDataLoader rawDataLoader = (RawDataLoader)target;

        // Show TF button
        if (GUILayout.Button("Load Raw Data"))
        {
            string file = EditorUtility.OpenFilePanel("Select a dataset to load", "DataFiles", "");
            if (File.Exists(file))
            {
                rawDataLoader.Load(file);
            }
            else
            {
                Debug.LogError("File doesn't exist: " + file);
            }
        }
    }
}
