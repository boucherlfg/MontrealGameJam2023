using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    const int size = 100;
    [Header("max counts")]
    public int maxEnemies = 100;
    public int maxCoin = 10;
    public int maxPotion = 5;
    public int maxPowerup = 10;
    
    [Header("spawn rates")]
    public float coinSpawnRate = 3;
    public float powerupSpawnRate = 5;
    public float potionSpawnRate = 10;
    public float baseEnemySpawnRate = 5;
    
    private float bufferTime = 5;
    private float enemySpawnRateCounter = 0;
    private float coinRateCounter = 0;
    private float potionRateCounter = 0;
    private float powerupRateCounter = 0;

    private int lastTotalEnemyCount; //for updating kill count
    private int killCount = 0;
    private int difficulty = 0;

    [Header("prefabs and container")]
    public Transform container;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public GameObject coinPrefab;
    public GameObject potionPrefab;
    public GameObject powerupPrefab;

    int Count<T>() where T : MonoBehaviour
    {
        int count = 0;
        foreach (Transform t in container)
        {
            if (t.GetComponent<T>()) count++;
        }
        return count;
    }
    // Update is called once per frame
    void Update()
    {
        ManageEnemies();
        SpawnCoins();
        SpawnPotion();
        SpawnPowerup();
    }
    void SpawnPowerup()
    {
        if (Count<PowerupScript>() > maxPowerup) return; 
        powerupRateCounter += Time.deltaTime;
        if (powerupRateCounter < powerupSpawnRate) return;
        powerupRateCounter = 0;

        var pos = GetValidPos();
        var powerup = Instantiate(powerupPrefab, pos, Quaternion.identity);
        powerup.transform.SetParent(container);

        var comp = powerup.GetComponent<PowerupScript>();

        for (int i = 0; i < difficulty; i++)
        {
            int choice = Random.Range(0, 7);
            switch (choice)
            {
                case 0:
                    comp.life++;
                    break;
                case 1:
                    comp.energy++;
                    break;
                case 2:
                    comp.energyRegen++;
                    break;
                case 3:
                    comp.moveSpeed++;
                    break;
                case 4:
                    comp.damage++;
                    break;
                case 5:
                    comp.attackSpeed++;
                    break;
                case 6:
                    comp.switchSpeed++;
                    break;
            }
        }
    }
    void SpawnPotion()
    {
        if (Count<PotionScript>() > maxPotion) return;
        potionRateCounter += Time.deltaTime;
        if (potionRateCounter < potionSpawnRate) return;
        potionRateCounter = 0;

        var pos = GetValidPos();
        var potion = Instantiate(potionPrefab, pos, Quaternion.identity);
        potion.transform.SetParent(container);

        var comp = potion.GetComponent<PotionScript>();
    }
    void SpawnCoins()
    {
        if (Count<CoinScript>() > maxCoin) return;
        coinRateCounter += Time.deltaTime;
        if (coinRateCounter < coinSpawnRate) return;
        coinRateCounter = 0;

        var pos = GetValidPos();
        var coin = Instantiate(coinPrefab, pos, Quaternion.identity);
        coin.transform.SetParent(container);
    }

    void ManageEnemies()
    {
        int enemyCount = Count<EnemyScript>();
        UpdateKillCount(enemyCount);

        if (enemyCount > maxEnemies) return;

        //give 5 seconds at start
        bufferTime -= Time.deltaTime;
        if (bufferTime > 0) return;

        //spawn only when spawn rate is respected
        difficulty = GetDifficulty(killCount);
        var enemySpawnRateModifier = GetEnemySpawnRateModifier(difficulty);
        enemySpawnRateCounter += Time.deltaTime;
        if (enemySpawnRateCounter < baseEnemySpawnRate * enemySpawnRateModifier) return;

        //reset spawn rate counter
        enemySpawnRateCounter = 0;

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
        var diff = lastTotalEnemyCount - mobCount;
        if (diff > 0) killCount += diff;
        lastTotalEnemyCount = mobCount;
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
    static float GetEnemySpawnRateModifier(int difficulty)
    {
        return 1f / (difficulty + 1f);
    }
}
