using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;


    // Start is called before the first frame update
    IEnumerator Start()
    {

        do
        {
          yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

     private IEnumerator  SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesWaves(currentWave));
        }
    }

    private IEnumerator  SpawnAllEnemiesWaves(WaveConfig waveConfig)
    {
        for (int enemyCount=0; enemyCount<waveConfig.GetNumberOfEnemies(); enemyCount++)
        {

            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            

            newEnemy.gameObject.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);


            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

}
