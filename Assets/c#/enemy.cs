using System.Collections;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform player;
    public float viewRadius = 15f;
    public float viewAngle = 120f;
    public float moveSpeed = 3f;
    public LayerMask obstacleMask;
    public int eneHp = 200;
    public int cost;
    public market marka;
    public int damageToPlayer = 15;

    private bool playerVisible = false;
    private Vector3 randomTarget;
    private bool isWandering = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            marka = playerObj.GetComponent<market>();
        }
        if (marka == null)
        {
            GameObject[] playersWithTag = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject obj in playersWithTag)
            {
                market possibleMarket = obj.GetComponent<market>();
                if (possibleMarket != null)
                {
                    marka = possibleMarket;
                    break;
                }
            }
        }

        StartCoroutine(WanderRoutine());
    }

    void Update()
    {
        playerVisible = IsPlayerInSight();

        if (playerVisible)
        {
            // Преследует игрока
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            RotateTowards(dir);
        }
        else if (isWandering)
        {
            // Двигается к случайной точке
            Vector3 dir = (randomTarget - transform.position).normalized;
            transform.position += dir * moveSpeed * 0.5f * Time.deltaTime; // медленнее, чем за игроком
            RotateTowards(dir);

            if (Vector3.Distance(transform.position, randomTarget) < 1f)
            {
                isWandering = false;
            }
        }

        if (eneHp == 0)
        {
            if (marka != null)
                marka.money += cost;
            Destroy(gameObject);
        }
    }

    void RotateTowards(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5);
        }
    }

    IEnumerator WanderRoutine()
    {
        while (true)
        {
            if (!playerVisible)
            {
                yield return new WaitForSeconds(Random.Range(1f, 3f));

                Vector3 wanderOffset = new Vector3(
                    Random.Range(-10f, 10f),
                    0,
                    Random.Range(-10f, 10f)
                );

                randomTarget = transform.position + wanderOffset;
                isWandering = true;
            }

            yield return null;
        }
    }

    bool IsPlayerInSight()
    {
        if (player == null) return false;

        Vector3 dirToPlayer = player.position - transform.position;
        float distToPlayer = dirToPlayer.magnitude;

        if (distToPlayer > viewRadius) return false;

        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        if (angle < viewAngle / 2f)
        {
            if (!Physics.Raycast(transform.position + Vector3.up * 1.5f, dirToPlayer.normalized, distToPlayer, obstacleMask))
            {
                return true;
            }
        }

        return false;
    }

    void OnCollisionEnter(Collision collision)
    {
        amoCont playerScript = collision.gameObject.GetComponent<amoCont>();
        if (playerScript != null)
        {
            playerScript.TakeDamage(damageToPlayer);
        }
    }

    public void TakeDamage(int amount)
    {
        eneHp -= amount;
        eneHp = Mathf.Clamp(eneHp, 0, 999);
    }
}