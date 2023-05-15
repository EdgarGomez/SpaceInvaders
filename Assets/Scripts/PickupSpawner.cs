using System.Collections;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public Pickup[] pickupPrefabs;
    public float spawnTime = 5f;
    public float spawnTimeVariation = 2f;

    void Start()
    {
        StartCoroutine(SpawnPickups());
    }

    IEnumerator SpawnPickups()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime + Random.Range(-spawnTimeVariation, spawnTimeVariation));

            Pickup pickupPrefab = pickupPrefabs[Random.Range(0, pickupPrefabs.Length)];
            float spawnPositionX = Random.Range(-Camera.main.aspect * Camera.main.orthographicSize, Camera.main.aspect * Camera.main.orthographicSize);
            Vector3 spawnPosition = new Vector3(spawnPositionX, transform.position.y, transform.position.z);

            Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
