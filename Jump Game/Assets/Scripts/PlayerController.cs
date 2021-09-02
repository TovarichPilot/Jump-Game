using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody PlayerRigidbody;
    private Animator playerAnimator;
    private AudioSource playerAudio;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float JumpForce = 10;
    public float GravityModifier;

    public int Score = 0;

    public bool isOnGround;
    public bool isInSky;
    public bool gameOver;
    public bool doubleSpeed = false;

    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        Physics.gravity *= GravityModifier; //use gravity
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)     // when the space is pressed and we are on the ground
        {
            PlayerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse); // first jump, when it has done then we are in the sky

            isInSky = true;
            isOnGround = false;

            playerAnimator.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

       

        if (PlayerRigidbody.position.y >= 2.5f && Input.GetKeyDown(KeyCode.Space) && isInSky) //double jump
        {
            PlayerRigidbody.AddForce(Vector3.up * JumpForce / 1.3f, ForceMode.Impulse);
            isInSky = false;
 
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            doubleSpeed = true;
            playerAnimator.SetFloat("Speed_Multiplier", 2.0f);
        }
        else if (doubleSpeed)
        {
            doubleSpeed = false;
            playerAnimator.SetFloat("Speed_Multiplier", 1.0f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", Random.Range(1, 3));
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }

}
