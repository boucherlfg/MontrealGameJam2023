using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Generator;

public class Generator : MonoBehaviour
{
    [System.Serializable]
    public class Generatable
    {
        public GameObject prefab;
        public int maxCount;
        public float spawnRate;
        public enum SpawnType { KillCount, Rate, ScoreCount }
        public SpawnType spawnType = SpawnType.KillCount;

        public bool isSubjectToDifficulty;
        [HideInInspector]
        public float spawnCounter;
        public int Count(Transform container)
        {
            int i = 0;
            foreach (Transform trans in container)
            {
                if (trans.name == prefab.name) i++;
            }
            if (prefab.gameObject.layer == LayerMask.NameToLayer("Points")) print(i);
            return i;
        }
        public float GetSpawnRateModifier(int difficulty)
        {
            return 1f / (difficulty + 1f);
        }
        public int GetDifficulty(int killCount)
        {
            return Mathf.FloorToInt(Mathf.Log(1 + killCount / 100f, 1.15f));
        }
        public void Create(Vector3 position, Transform container, System.Action<GameObject> action = null)
        {
            var instance = Instantiate(prefab, position, Quaternion.identity);
            instance.transform.SetParent(container);
            instance.name = prefab.name;
            action?.Invoke(instance);
        }
    }

    public Generatable[] generatables;
    public const int size = 25;

    [Header("prefabs and container")]
    public Transform container;

    void OnEnable()
    {
        Gamestate.Instance.Environment.TotalKill.Changed += Kills_Changed;
        Gamestate.Instance.Environment.Score.Changed += Score_Changed;
        
    }

    
    void OnDisable()
    {
        Gamestate.Instance.Environment.TotalKill.Changed -= Kills_Changed;
        Gamestate.Instance.Environment.Score.Changed -= Score_Changed;
    }
    private void Score_Changed()
    {
        if (Gamestate.Instance.Paused.Value)
        {
            return;
        }

        foreach (var gen in generatables)
        {
            if (gen.spawnType != Generatable.SpawnType.ScoreCount) continue;
            if (Gamestate.Instance.Props[gen.prefab.name] >= gen.maxCount) continue;

            var difficulty = gen.GetDifficulty(Gamestate.Instance.Environment.TotalKill.Value);
            var spawnRateModifier = gen.isSubjectToDifficulty ? gen.GetSpawnRateModifier(difficulty) : 1;

            var score = Gamestate.Instance.Environment.Score.Value;
            if (score <= 0 || score % (int)(gen.spawnRate / spawnRateModifier) != 0) continue;


            gen.spawnCounter = 0;

            var pos = GetValidPos();
            gen.Create(pos, container, obj => {
                var comp = obj.GetComponent<PowerupScript>();
                if (comp) BuildPowerup(comp, difficulty);
            });
        }
    }

    private void Kills_Changed()
    {
        if (Gamestate.Instance.Paused.Value)
        {
            return;
        }

        foreach (var gen in generatables)
        {
            if (gen.spawnType != Generatable.SpawnType.KillCount) continue;
            if (Gamestate.Instance.Props[gen.prefab.name] >= gen.maxCount) continue;

            var difficulty = gen.GetDifficulty(Gamestate.Instance.Environment.TotalKill.Value);
            var spawnRateModifier = gen.isSubjectToDifficulty ? gen.GetSpawnRateModifier(difficulty) : 1;

            var totalKill = Gamestate.Instance.Environment.TotalKill.Value;
            if (totalKill <= 0 || totalKill % (int)(gen.spawnRate/spawnRateModifier) != 0) continue;
            

            gen.spawnCounter = 0;

            var pos = GetValidPos();
            gen.Create(pos, container, obj => {
                var comp = obj.GetComponent<PowerupScript>();
                if (comp) BuildPowerup(comp, difficulty);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamestate.Instance.Paused.Value)
        {
            return;
        }

        foreach (var gen in generatables)
        {
            if (gen.spawnType != Generatable.SpawnType.Rate) continue;
            if (Gamestate.Instance.Props[gen.prefab.name] >= gen.maxCount) continue;
            
            var difficulty = gen.GetDifficulty(Gamestate.Instance.Kills[gen.prefab.name]);
            gen.spawnCounter += Time.deltaTime;
            var spawnRateModifier = gen.GetSpawnRateModifier(difficulty);

            if (gen.spawnCounter < (gen.isSubjectToDifficulty ? spawnRateModifier : 1) * gen.spawnRate) continue;

            gen.spawnCounter = 0;

            var pos = GetValidPos();
            gen.Create(pos, container, obj => {
                var comp = obj.GetComponent<PowerupScript>();
                if (comp) BuildPowerup(comp, difficulty);
            });
        }
    }
    void BuildPowerup(PowerupScript comp, int difficulty)
    {
        float powerLevel = GetPowerLevel(difficulty);

        for (int i = 0; i < powerLevel; i++)
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
            }
        }
    }

    static Vector2 GetValidPos()
    {
        var pos = Gamestate.Instance.Player.Position.Value + Random.insideUnitCircle.normalized * 20;
        while (pos.x < -size || pos.x > size || pos.y < -size || pos.y > size)
        {
            pos = Gamestate.Instance.Player.Position.Value + Random.insideUnitCircle.normalized * 20;
        }

        var hit = Physics2D.OverlapCircleAll(pos, 0.3f);
        if (hit.Length > 0) return GetValidPos();

        return pos;
    }
    
    static int GetPowerLevel(int difficulty)
    {
        return (int)Mathf.Log(difficulty + 1, 3) + 1;
    }
}
