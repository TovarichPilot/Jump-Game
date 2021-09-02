using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftObstacle : MonoBehaviour
{
    public float speed;
    private PlayerController playerControllerScript;
    public float leftBoundary = - 15;
    public int missedObstacles = 0;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>(); // need to reference to the GameOver
    }

    
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            // just move

            if (playerControllerScript.doubleSpeed)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed * 1.5f);
                Debug.Log("Dush!");
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }

        if (transform.position.x < leftBoundary && gameObject.CompareTag("Obstacle"))
        {
            missedObstacles++;
            Destroy(gameObject);
        }
    }
}