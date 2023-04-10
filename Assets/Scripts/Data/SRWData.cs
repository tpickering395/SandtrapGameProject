using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DGU/SimpleRandomWalkData", fileName = "SimpleRandomWalkParameters_")]
public class SRWData : ScriptableObject
{
    public int iterations = 10, pathLength = 10;
    public bool RandomizeInitIterations = true;
}
