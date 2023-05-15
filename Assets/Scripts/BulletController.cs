using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public GameObject explosionPrefab;
    public bool isMissile = false; // Add this to know if bullet is a missile
    public float explosionRadius = 1f; // Radius in which missile will destroy enemies

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.IncrementEnemies();

            // If this bullet is a missile, destroy nearby enemies
            if (isMissile)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Enemy"))
                    {
                        Destroy(collider.gameObject);
                    }
                }
            }

            Instantiate(explosionPrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}





