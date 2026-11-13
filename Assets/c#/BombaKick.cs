using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaKick : MonoBehaviour
{
    public GameObject bombaPrefab;
    public Transform KikcPoint;
    public float BombSpeed = 20f;
    public float BombRate = 0.2f;
    private float nextTimeToFire = 0f;
    public GameObject ReadyS;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + BombRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bomb = Instantiate(bombaPrefab, KikcPoint.position, KikcPoint.rotation);
        if(menu.isMusic)
        {
            Instantiate(ReadyS, transform.position, Quaternion.identity);
        }            
        StartCoroutine(HideTemporarily());
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = KikcPoint.forward * BombSpeed;
        }
    }

    IEnumerator HideTemporarily()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            rend.enabled = false;
        }

        yield return new WaitForSeconds(4f);

        foreach (Renderer rend in renderers)
        {
            rend.enabled = true;
        }
    }
}