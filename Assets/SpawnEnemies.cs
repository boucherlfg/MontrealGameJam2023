using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public float spawnSpeed = 2;
    public GameObject spawnPrefab;
    private float spawnCooldown = 0;

    // Update is called once per frame
    void Update()
    {
        if (Gamestate.Instance.Paused) return;
        spawnCooldown += Time.deltaTime;
        if (spawnCooldown < spawnSpeed) return;

        spawnCooldown = 0;
        var delta = Random.insideUnitCircle.normalized;
        var instance = Instantiate(spawnPrefab, transform.position +(Vector3) delta, Quaternion.identity);
        instance.transform.SetParent(transform.parent);
        instance.name = spawnPrefab.name + "Spawn";
    }
}
