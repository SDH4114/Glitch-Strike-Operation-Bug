using UnityEngine;

public class water : MonoBehaviour
{
    public int damageAmount = 5;
    public float damageInterval = 2f;

    private bool playerInWater = false;
    private float timer = 0f;
    private amoCont player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<amoCont>();
            if (player != null)
            {
                playerInWater = true;
                timer = damageInterval;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInWater = false;
            player = null;
            timer = 0f;
        }
    }

    void Update()
    {
        if (playerInWater && player != null)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                player.TakeDamage(damageAmount);
                timer = damageInterval;
            }
        }
    }
}