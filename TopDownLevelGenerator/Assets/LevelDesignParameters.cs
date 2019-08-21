using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// Keeps track of per-level variables for level generation
public class LevelDesignParameters : MonoBehaviour
{
    [SerializeField]
    public GameObject LevelFloorTile;

    public static GameObject FloorTile { get; private set; }

    [SerializeField]
    public GameObject LevelWallTile;

    public static GameObject WallTile { get; private set; }

    [SerializeField]
    public Color LevelFloorColor = Color.white;

    public static Color FloorColor { get; private set; }

    [SerializeField]
    public Color LevelWallColor = Color.black;

    public static Color WallColor { get; private set; }

    [SerializeField]
    public Color LevelExitColor = Color.green;

    public static Color ExitColor { get; private set; }

    [SerializeField]
    public int MinTiles = 100;

    [SerializeField]
    public int MaxTiles = 10000;

    public static int TargetTileCount { get; private set; }

    public static int TileCount;
    public static int TileCountCheck => FindObjectsOfType<TileBehavior>().Length;

    [SerializeField]
    public int MinTilesBeforeLevelExit = 1;

    public static int MinTilesBeforeExit { get; private set; }

    [SerializeField]
    public float MinDistanceBeforeLevelExit = 15.0f;

    public static float MinDistanceBeforeExit { get; private set; }

    [SerializeField]
    public float LevelExitChance = 0.01f;
    
    public static float ExitChance { get; private set; }

    public static bool HasExit;
    public static bool HasExitCheck => GameObject.FindGameObjectsWithTag("Exit").Length > 1;

    [SerializeField]
    public float MinBranchChance = 0.05f;

    [SerializeField]
    public float MaxBranchChance = 0.4f;

    public static int BranchCount = 0;

    [SerializeField]
    public int MinMaxAllowedBranches = 0;

    [SerializeField]
    public int MaxMaxAllowedBranches = 10;

    public static int MaxBranches = 0;

    public static float BranchChance { get; private set; }

    public static bool LevelReady => HasExit && TileCount >= TargetTileCount;
    
    // Start is called before the first frame update
    void Start()
    {
        TileCount = 0;
        BranchCount = 0;
        HasExit = false;
        // Aim for a certain number of tiles
        TargetTileCount = Random.Range(MinTiles, MaxTiles + 1);
        Debug.Log($"Target Floor Tile Count: {TargetTileCount}");
        // Aim for a certain Branch Chance
        BranchChance = Random.Range(MinBranchChance, MaxBranchChance);
        Debug.Log($"Branch Chance: {BranchChance}");
        // Aim for a certain max number of branches
        MaxBranches = Random.Range(MinMaxAllowedBranches, MaxMaxAllowedBranches + 1);
        Debug.Log($"Max allowed branches: {MaxBranches}");
        // Use static properties to improve ease of access
        FloorTile = LevelFloorTile;
        FloorColor = LevelFloorColor;
        WallTile = LevelWallTile;
        WallColor = LevelWallColor;
        ExitColor = LevelExitColor;
        MinTilesBeforeExit = MinTilesBeforeLevelExit;
        MinDistanceBeforeExit = MinDistanceBeforeLevelExit;
        ExitChance = LevelExitChance;
        // Create the first tile. The tile should be set up to do the rest
        Instantiate(FloorTile, new Vector3(0, 0, 0), new Quaternion(0,0,0,0));
    }
}
