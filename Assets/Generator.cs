using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [System.Serializable]
    public class Generatable
    {
        public GameObject prefab;
        public int maxCount;
        public float spawnRate;

        public bool isSubjectToDifficulty;
        [HideInInspector]
        public float spawnCounter;

        private int lastCount;
        public int killCount { get; private set; }
        public int Count(Transform container)
        {
            int i = 0;
            foreach (Transform trans in container)
            {
                if (trans.name == prefab.name) i++;
            }
            return i;
        }
        public void UpdateKillCount(int count)
        {
            var diff = lastCount - count;
            if (diff > 0) killCount += diff;
            lastCount = count;
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
            position.z = -100;
            var instance = Instantiate(prefab, position, Quaternion.identity);
            instance.transform.SetParent(container);
            action?.Invoke(instance);
        }
    }

    public Generatable[] generatables;
    const int size = 50;

    [Header("prefabs and container")]
    public Transform container;

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
        if (Gamestate.Instance.Paused) 
        {
            return;
        }

        foreach (var gen in generatables)
        {
            int count = gen.Count(container);
            gen.UpdateKillCount(count);

            if (count >= gen.maxCount) continue;
            
            var difficulty = gen.GetDifficulty(gen.killCount);
            var spawnRateModifier = gen.GetSpawnRateModifier(difficulty);
            gen.spawnCounter += Time.deltaTime;
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
                case 6:
                    comp.switchSpeed++;
                    break;
            }
        }
    }

    static Vector2 GetValidPos()
    {
        var pos = Gamestate.Instance.Position + Random.insideUnitCircle.normalized * 20;
        while (pos.x < -size || pos.x > size || pos.y < -size || pos.y > size)
        {
            pos = Gamestate.Instance.Position + Random.insideUnitCircle.normalized * 20;
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
