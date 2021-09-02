using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float score;
    public float lerpingSpeed;

    public Transform startingPoint;

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;

        playerControllerScript.gameOver = true;
        StartCoroutine(PlayIntro());
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            if (playerControllerScript.doubleSpeed)
            {
                score += 2;
            }
            else
            {
                score++;
            }
            Debug.Log(score);
        }
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPosition = playerControllerScript.transform.position;
        Vector3 finishPosition = startingPoint.position;

        float journeyLenght = Vector3.Distance(startPosition, finishPosition);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpingSpeed;
        float fractionOfJourney = distanceCovered / journeyLenght;

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);


        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpingSpeed;
            fractionOfJourney = distanceCovered / journeyLenght;
            playerControllerScript.transform.position = Vector3.Lerp(startPosition, finishPosition, fractionOfJourney);
            yield return null;
        }

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);

        playerControllerScript.gameOver = false;
    }
}
