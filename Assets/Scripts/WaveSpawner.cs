using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    private bool waveOut = false;
    public GameManager gameManager;
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    public Transform spawnPoint;
    public GameObject spawnEffect;

    public float timeBetweenWaves = 1f;
    private float countdown = 2f;
    [SerializeField] public Text waveCountdownText;

    private int waveIndex = 0;


    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length && EnemiesAlive <= 0)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown > 0 && countdown < 0.99)
        {
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            waveCountdownText.text = string.Format("{0:0.00}", countdown);
        }
        else if (countdown >= 0 && !waveOut)
        {
            waveCountdownText.text = Mathf.FloorToInt(countdown + 1).ToString();
        }
        else if (countdown >= -1)
        {
            waveCountdownText.text = "Wave " + (waveIndex+1);
        }
        else if (countdown <= -2)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;



    }

    IEnumerator SpawnWave()
    {
        waveOut = true;
        GameObject effect = (GameObject)Instantiate(spawnEffect, spawnPoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        PlayerStats.Rounds++;
        Wave wave = waves[waveIndex];
        EnemiesAlive = wave.enemies.Length * wave.count;
        
        for (int i = 0; i < wave.count; i++)
        {
            for (int j = 0; j < wave.enemies.Length; j++)
            {
                SpawnEnemy(wave.enemies[j]);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }
        
        waveIndex++;
        Destroy(effect, 1f);

        waveOut = false;
        
    }

    void SpawnEnemy(GameObject enemy)
    {
        
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);

    }
}
