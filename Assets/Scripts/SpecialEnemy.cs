using UnityEngine;

public class SpecialEnemy : Enemy
{
    public override void Shoot()
    {
        if (enemyBulletPrefab != null)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = Vector2.down * bulletSpeed;

            float angle = 25f * Mathf.Deg2Rad;
            Vector2 directionLeft = new Vector2(-Mathf.Sin(angle), -Mathf.Cos(angle));
            Vector2 directionRight = new Vector2(Mathf.Sin(angle), -Mathf.Cos(angle));

            GameObject leftBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, -25));
            Rigidbody2D leftBulletRb = leftBullet.GetComponent<Rigidbody2D>();
            leftBulletRb.velocity = directionLeft * bulletSpeed;

            GameObject rightBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, 25));
            Rigidbody2D rightBulletRb = rightBullet.GetComponent<Rigidbody2D>();
            rightBulletRb.velocity = directionRight * bulletSpeed;
        }
    }


}
