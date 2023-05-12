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


        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -2.5f, 2.5f);
        transform.position = pos;

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }
}
