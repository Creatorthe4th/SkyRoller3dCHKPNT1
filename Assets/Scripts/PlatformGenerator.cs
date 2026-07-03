using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public PlatformSegment safePrefab;        // plain platform: starting runway + default
    public PlatformSegment[] hazardPrefabs;   // Slow, Ice, Saw (NOT plain)

    [Header("Spawning")]
    public int safeStartCount = 4;            // hazard-free, centered segments to begin with
    public float spawnAheadDistance = 80f;    // keep this much track in front of the player
    public float despawnBehindDistance = 30f; // remove platforms this far behind the player
    [Tooltip("Chance a post-runway segment carries a hazard. Rest are plain.")]
    [Range(0f, 1f)] public float hazardChance = 0.25f;
    public bool avoidImmediateRepeats = true;

    [Header("Lateral Variety")]
    [Tooltip("Width of one block. Lanes are locked to multiples of this.")]
    public float laneWidth = 2f;
    [Tooltip("Furthest lane from center. 2 => x can be -4,-2,0,2,4.")]
    public int maxLanes = 2;
    [Tooltip("Chance per segment that the lane shifts one block left/right.")]
    [Range(0f, 1f)] public float laneChangeChance = 0.25f;

    readonly Queue<PlatformSegment> active = new Queue<PlatformSegment>();
    Vector3 nextSpawnPos;
    int spawnedCount;
    int currentLane;
    bool laneChangedThisStep;
    PlatformSegment lastPrefab;

    void Start()
    {
        nextSpawnPos = transform.position; // generator sits at the player's start, z = 0
        while (nextSpawnPos.z < player.position.z + spawnAheadDistance)
            SpawnNext();
    }

    void Update()
    {
        while (nextSpawnPos.z < player.position.z + spawnAheadDistance)
            SpawnNext();

        while (active.Count > 0 &&
               active.Peek().EndPosition().z < player.position.z - despawnBehindDistance)
        {
            Destroy(active.Dequeue().gameObject);
        }
    }

    void SpawnNext()
    {
        // Discrete lane hop: move exactly one block-width left or right.
        laneChangedThisStep = false;
        if (spawnedCount >= safeStartCount && Random.value < laneChangeChance)
        {
            int dir = Random.value < 0.5f ? -1 : 1;
            int newLane = Mathf.Clamp(currentLane + dir, -maxLanes, maxLanes);
            laneChangedThisStep = (newLane != currentLane);
            currentLane = newLane;
        }
        nextSpawnPos.x = currentLane * laneWidth;

        PlatformSegment prefab = ChoosePrefab();
        PlatformSegment seg = Instantiate(prefab);
        seg.PlaceStartAt(nextSpawnPos);

        nextSpawnPos.z = seg.EndPosition().z; // advance z; x is driven by the lane
        active.Enqueue(seg);
        spawnedCount++;
        lastPrefab = prefab;
    }

    PlatformSegment ChoosePrefab()
    {
        // Always plain during the runway, and on the segment where we just hopped
        // lanes (keeps the trickiest diagonal transition fair).
        if (spawnedCount < safeStartCount || laneChangedThisStep)
            return safePrefab;

        if (hazardPrefabs == null || hazardPrefabs.Length == 0)
            return safePrefab;

        if (Random.value >= hazardChance)
            return safePrefab; // most segments stay plain

        PlatformSegment pick = hazardPrefabs[Random.Range(0, hazardPrefabs.Length)];
        if (avoidImmediateRepeats && hazardPrefabs.Length > 1)
        {
            int guard = 0;
            while (pick == lastPrefab && guard++ < 5)
                pick = hazardPrefabs[Random.Range(0, hazardPrefabs.Length)];
        }
        return pick;
    }
}