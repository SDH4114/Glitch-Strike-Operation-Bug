using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;

    public float startDelay = 3f;
    public float minDelay = 0.3f;
    public float acceleration = 0.01f;

    public float currentDelay;

    void Start()
    {
        currentDelay = startDelay;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnRandomPrefab();

            yield return new WaitForSeconds(currentDelay);
            currentDelay = Mathf.Max(minDelay, currentDelay - acceleration * Time.deltaTime);
        }
    }

    void SpawnRandomPrefab()
    {
        if (prefabsToSpawn.Length == 0)
            return;

        GameObject prefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
