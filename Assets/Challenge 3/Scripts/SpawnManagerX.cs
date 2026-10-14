using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;

    private float spawnDelay = 2.0f;
    private float minSpawnInterval = 1.0f;
    private float maxSpawnInterval = 5.0f;

    private PlayerControllerX playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Find the player controller script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();

        // Start the recursive spawn cycle after the initial delay
        Invoke("SpawnObjects", spawnDelay);
    }

    // Spawn obstacles
    void SpawnObjects()
    {
        // If game is still active, spawn new object
        if (playerControllerScript != null && !playerControllerScript.gameOver)
        {
            // Set random spawn location and random object index
            Vector3 spawnLocation = new Vector3(30, Random.Range(5, 15), 0);
            int index = Random.Range(0, objectPrefabs.Length);

            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);

            // Randomize the next spawn time for unpredictable gameplay
            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            Invoke("SpawnObjects", randomInterval);
        }
    }
}