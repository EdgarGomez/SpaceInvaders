using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;

    private float nextFire = 0.0f;

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);

        GetComponent<Rigidbody2D>().velocity = movement * speed;

        // Prevent player from leaving screen
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -8, 8); // adjust these values as per your screen size
        transform.position = pos;

        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }
}
