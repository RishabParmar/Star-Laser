using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfiguration : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    float timeBetweenSpawns = 0.5f;
    int numberOfEnemies = 5;
    float moveSpeed = 4f;

    public GameObject getEnemyPrefab() { return enemyPrefab; }
    public List<Transform> getWayPoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform wayPoint in pathPrefab.transform)
        {
            waveWaypoints.Add(wayPoint);
        }
        return waveWaypoints;
    }
    public float getTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float getMoveSpeed() { return moveSpeed; }
    public int getNumberOfEnemies() { return numberOfEnemies; }
}
