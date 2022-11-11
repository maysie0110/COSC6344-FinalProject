using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AddSlicePlaneBtn))]
public class AddSlicePlaneCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AddSlicePlaneBtn addSlice = (AddSlicePlaneBtn)target;

        // Show TF button
        if (GUILayout.Button("Add slice"))
        {
            addSlice.OnBtnClick();
        }
    }
}
