using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractProceduralGenerator), true)]
public class RDGEditor : Editor
{
    AbstractProceduralGenerator generator;

    private void Awake()
    {
        generator = (AbstractProceduralGenerator) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }
    }
}
