using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{    
    [SerializeField] List<WaveConfig> waveConfigs;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while(true)
        {
            yield return StartCoroutine(SpawnAllWaves());
        }        
    }

    IEnumerator SpawnAllWaves()
    {
        for(int i=0;i< waveConfigs.Count;i++)
        {
            var currentWave = waveConfigs[i];          
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
        
    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int i=0;i < waveConfig.getNumberOfEnemies();i++)
        {            
            var newEnemy = Instantiate(waveConfig.getEnemyPrefab(), waveConfig.getWayPoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.getTimeBetweenSpawns());
        }
    }
}
