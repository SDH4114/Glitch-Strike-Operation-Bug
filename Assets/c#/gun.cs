using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform[] firePoints;
    public float bulletSpeed = 20f;
    public float fireRate = 0.2f;

    public GameObject[] shotS;
    public GameObject[] reloadS;

    public int MaxCountB;
    public float reloadDelay = 1f;
    public int CountB;
    public int AllCountB;
    public bool isReloading = false;

    public TextMeshProUGUI CountTe;
    public Image reloadProgress;
    private float nextTimeToFire = 0f;
    public TextMeshProUGUI AllTe;

    public Camera playerCamera;
    public float zoomFOV = 30f;
    public float normalFOV = 60f;
    public float zoomSpeed = 10f; 

    void Start()
    {
        CountB = MaxCountB;
        if (reloadProgress != null)
            reloadProgress.fillAmount = 0f;
    }

    void Update()
    {
        UpdateBulletUI();
        if (playerCamera != null)
        {
            float targetFOV = Input.GetMouseButton(1) ? zoomFOV : normalFOV;
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
        }
        if (isReloading)
            return;

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            if (CountB > 0)
            {
                CountB--;
                nextTimeToFire = Time.time + fireRate;
                Shoot();
            }
        }
        if ((CountB == 0 || Input.GetKeyDown(KeyCode.R)) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        foreach (Transform point in firePoints)
        {
            GameObject bullet = Instantiate(bulletPrefab, point.position, point.rotation);
            if(menu.isMusic)
            {
                int index = Random.Range(0, shotS.Length);
                Instantiate(shotS[index], Vector3.zero, Quaternion.identity);
            }
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = point.forward * bulletSpeed;
            }
        }
    }

    IEnumerator Reload()
    {
        if (CountB != MaxCountB && AllCountB > 0)
        {
            isReloading = true;

            float elapsed = 0f;

            while (elapsed < reloadDelay)
            {
                elapsed += Time.deltaTime;
                if (reloadProgress != null)
                    reloadProgress.fillAmount = elapsed / reloadDelay;

                yield return null;
            }

            if (reloadProgress != null)
                reloadProgress.fillAmount = 0f;

            int bulletsToReload = Mathf.Min(MaxCountB - CountB, AllCountB);
            CountB += bulletsToReload;
            AllCountB -= bulletsToReload;

            if(menu.isMusic)
            {
                int Ondex = Random.Range(0, reloadS.Length);
                Instantiate(reloadS[Ondex], Vector3.zero, Quaternion.identity);
            }

            isReloading = false;
        }
    }

    void UpdateBulletUI()
    {
        CountTe.text = CountB.ToString();
        AllTe.text = AllCountB.ToString();
    }

    void OnDisable()
    {
        isReloading = false;

        if (reloadProgress != null)
            reloadProgress.fillAmount = 0f;
    }
}