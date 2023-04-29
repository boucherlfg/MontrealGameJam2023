using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    const int size = 100;
    const int maxEnemies = 100;
    
    public float coinSpawnRate = 3;
    public float powerupSpawnRate = 5;
    
    private float bufferTime = 5;
    private float spawnRateCounter = 0;
    private float coinRateCounter = 0;
    private int lastCount;
    private int killCount = 0;

    public Transform container;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public GameObject coinPrefab;
    

    // Update is called once per frame
    void Update()
    {
        ManageEnemies();
        SpawnCoins();
    }

    void SpawnCoins()
    {
        coinRateCounter += Time.deltaTime;
        if (coinRateCounter < coinSpawnRate) return;
        coinRateCounter = 0;

        var pos = GetValidPos();
        var coin = Instantiate(coinPrefab, pos, Quaternion.identity);
        coin.transform.SetParent(container);
    }

    void ManageEnemies()
    {
        int count = 0;
        foreach (Transform t in container)
        {
            count++;
        }
        UpdateKillCount(count);

        if (count > maxEnemies) return;

        //give 5 seconds at start
        bufferTime -= Time.deltaTime;
        if (bufferTime > 0) return;

        //spawn only when spawn rate is respected
        var difficulty = GetDifficulty(killCount);
        var spawnRate = GetSpawnRate(difficulty);
        spawnRateCounter += Time.deltaTime;
        if (spawnRateCounter < spawnRate) return;

        //reset spawn rate counter
        spawnRateCounter = 0;

        //generate enemy
        var pos = GetValidPos();
        var dice = Random.Range(0, 10);

        if (dice < 3)
        {
            var instance = Instantiate(bossPrefab, pos, Quaternion.identity);
            instance.transform.SetParent(container);
        }
        else
        {
            var instance = Instantiate(enemyPrefab, pos, Quaternion.identity);
            instance.transform.SetParent(container);
        }
    }

    void UpdateKillCount(int mobCount)
    {
        var diff = lastCount - mobCount;
        if (diff > 0) killCount += diff;
        lastCount = mobCount;
    }

    static Vector2 GetValidPos()
    {
        var pos = Gamestate.Instance.Player.Position + Random.insideUnitCircle.normalized * 20;
        while (pos.x < -size || pos.x > size || pos.y < -size || pos.y > size)
        {
            pos = Gamestate.Instance.Player.Position + Random.insideUnitCircle.normalized * 20;
        }

        var hit = Physics2D.OverlapCircleAll(pos, 0.3f);
        if (hit.Length > 0) return GetValidPos();

        return pos;
    }
    static int GetDifficulty(int killCount)
    {
        return Mathf.FloorToInt(Mathf.Log(1 + killCount / 100f, 1.15f));
    }
    static float GetSpawnRate(int difficulty)
    {
        return 1f / (difficulty + 1f);
    }
}
