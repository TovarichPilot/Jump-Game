using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPosition = new Vector3(25, 0, 0);
    public float startDelay = 2;
    public float repeatRate = 2;
    private PlayerController playerControllerScript;
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        int index = Random.Range(1, obstaclePrefab.Length);

        if (playerControllerScript.gameOver == false)
        {
            if (index == 3)
            {
                spawnPosition = new Vector3(25, 1.5f, 0);
                Instantiate(obstaclePrefab[index], spawnPosition, obstaclePrefab[index].transform.rotation);
            }
            else
            {
                spawnPosition = new Vector3(25, 0, 0);
                Instantiate(obstaclePrefab[index], spawnPosition, obstaclePrefab[index].transform.rotation);              
            }
        }

        DelayRandom();
    }

    // set the delay and frequency of instantion of objects
    void DelayRandom()
    {
        startDelay = Random.Range(0, 2);
        repeatRate = Random.Range(0, 3);

    }
}
