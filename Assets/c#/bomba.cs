using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomba : MonoBehaviour
{
    public float explosionDelay = 3f;
    public GameObject smokeEffectPrefab;
    public float smokeDuration = 5f;
    public GameObject SmokeS;

    void Start()
    {
        Invoke("Explode", explosionDelay);
    }

    void Explode()
    {
        GameObject smoke = Instantiate(smokeEffectPrefab, transform.position, Quaternion.identity);
        if(menu.isMusic)
        {
            Instantiate(SmokeS, transform.position, Quaternion.identity);
        }
        Destroy(smoke, smokeDuration);
        Destroy(gameObject); 
    }
}
