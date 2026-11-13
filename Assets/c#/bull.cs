using UnityEngine;

public class bull : MonoBehaviour
{
    public int damageAmount = 1;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            amoCont player = collision.gameObject.GetComponent<amoCont>();
            enemy enem = collision.gameObject.GetComponent<enemy>();

            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
            if (enem != null)
            {
                enem.TakeDamage(damageAmount);
            }
            Destroy(gameObject);
        }
    }
}