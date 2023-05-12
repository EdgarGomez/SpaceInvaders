using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyController enemyController;
    public GameObject turboFireLeft;
    public GameObject turboFireRight;
    public GameObject enemyBulletPrefab;
    public float bulletSpeed = 5f;

    void Start()
    {
        turboFireLeft.SetActive(false);
        turboFireRight.SetActive(false);
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, enemyBulletPrefab.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border"))
        {
            enemyController.HandleBorderCollision();
        }
    }

    public void ShowTurboFire()
    {
        turboFireLeft.SetActive(true);
        turboFireRight.SetActive(true);
    }

    public void HideTurboFire()
    {
        turboFireLeft.SetActive(false);
        turboFireRight.SetActive(false);

    }
}
