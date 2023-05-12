using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}




