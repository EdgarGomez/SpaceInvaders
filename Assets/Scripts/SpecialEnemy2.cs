using UnityEngine;

public class SpecialEnemy2 : Enemy
{
    public override void Shoot()
    {
        if (enemyBulletPrefab != null)
        {
            for (int i = -2; i <= 2; i++)
            {
                float angle = 15f * i;
                Vector2 direction = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), -Mathf.Cos(angle * Mathf.Deg2Rad));

                GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                bulletRb.velocity = direction * bulletSpeed;
            }
        }
    }
}