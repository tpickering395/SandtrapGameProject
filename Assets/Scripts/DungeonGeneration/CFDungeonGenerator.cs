using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CFDungeonGenerator : DungeonGeneration
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;
    [SerializeField]
    public SRWData generationParameters;
    protected override void StartProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorCoords = new HashSet<Vector2Int>();
        HashSet<Vector2Int> roomCandidatePositions = new HashSet<Vector2Int>();

        CreateCorridors(floorCoords, roomCandidatePositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(roomCandidatePositions);

        List<Vector2Int> deadEnds = FindDeadEnds(floorCoords);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorCoords.UnionWith(roomPositions);

        mapVisualizer.PaintFloor(floorCoords);
        WallGenerator.CreateWalls(floorCoords, mapVisualizer);
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if(!roomFloors.Contains(position))
            {
                var room = GenerateRandomWalk(externalParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindDeadEnds(HashSet<Vector2Int> floorCoords)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorCoords) 
        {
            int adjacencyCount = 0;
            foreach (var dir in Direction2D.directionEnums)
            {
                if(floorCoords.Contains(position + dir))
                {
                    adjacencyCount++;
                }
            }
            if(adjacencyCount == 1)
            {
                deadEnds.Add(position);
            }
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> roomCandidatePositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomQuota = Mathf.RoundToInt(roomCandidatePositions.Count * roomPercent);

        List<Vector2Int> roomToCreate = roomCandidatePositions.OrderBy(x => Guid.NewGuid()).Take(roomQuota).ToList();

        foreach (var roomPosition in roomToCreate)
        {
            var roomFloor = GenerateRandomWalk(externalParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorCoords, HashSet<Vector2Int> roomCandidatePositions)
    {
        var currentPosition = initialPos;
        roomCandidatePositions.Add(currentPosition);
        for (int i = 0; i < corridorCount; i++)
        {
            var path = ProceduralDungeonGenerationAlgorithms.RWCorridor(currentPosition, corridorLength);
            currentPosition = path[path.Count - 1];
            roomCandidatePositions.Add(currentPosition);
            floorCoords.UnionWith(path);
        }
    }
}
