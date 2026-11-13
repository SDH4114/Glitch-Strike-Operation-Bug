using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrier : MonoBehaviour
{
    public GameObject barPrefab;
    public Transform kickPoint;
    public float barSpeed = 20f;
    public float barRate = 0.2f;
    private float nextTimeToFire = 0f;
    public GameObject readySign;
    public static int barCount = 0;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + barRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (barPrefab == null || kickPoint == null) return;

        GameObject barr = Instantiate(barPrefab, kickPoint.position, kickPoint.rotation);
        if(menu.isMusic)
        {
           Instantiate(readySign, transform.position, Quaternion.identity);
        }
        if (rend != null)
        {
            StartCoroutine(HideTemporarily());
        }

        Rigidbody rb = barr.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = kickPoint.forward * barSpeed;
        }
    }

    IEnumerator HideTemporarily()
    {
        rend.enabled = false;
        yield return new WaitForSeconds(8f);
        rend.enabled = true;
    }
}