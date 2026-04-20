using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProceduralGenerator : MonoBehaviour
{
    [SerializeField]
    protected GenerationVisualizer mapVisualizer = null;

    [SerializeField]
    protected Vector2Int initialPos = Vector2Int.zero;

    public void GenerateDungeon()
    {
        mapVisualizer.Clear();
        StartProceduralGeneration();
    }

    protected abstract void StartProceduralGeneration();
}
