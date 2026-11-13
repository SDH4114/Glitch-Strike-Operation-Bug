using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawner : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;
    public float destroyAfterSeconds = 5f;

    void Start()
    {
        if (prefabsToSpawn.Length == 0)
            return;

        GameObject prefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
        GameObject spawned = Instantiate(prefab, transform.position, transform.rotation);
        Destroy(gameObject, destroyAfterSeconds);

    }
}
