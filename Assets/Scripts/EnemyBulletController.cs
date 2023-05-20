using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Border") || other.gameObject.CompareTag("Shield") || other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}




