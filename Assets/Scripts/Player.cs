using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Modifying this variable lets you manipulate the player's jump height
    public float jumpHeight;

    //Modifying this variable lets you manipulate the player's health
    public int playerHealth;

    //Rigidbody2D variable that holds the player's rigidbody, allowing us to manipulate it by adding forces
    private Rigidbody2D playerRigidbody;

    //Boolean that detects if the player is grounded
    private bool isGrounded;

    //Float timer mechanism to prevent any accidental double jumps
    private float dontResetGroundedTimer = 0.5f;
    private float dontResetGroundedTime = 0;

    public float playerInvincibilityFrameLength = 2f;
    private float playerInvincibilityTime = 0;
    private bool isPlayerInvincible = false;

    // Start is called before the first frame update
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        GameManager.instance.UpdateHealthUI(playerHealth);
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetButtonDown("Jump") && isGrounded && !GameManager.instance.GetGameOver()){
            Jump();
        }

        RunGroundedTimer();
    }

    //Helper function that handles jumping code
    private void Jump(){
        playerRigidbody.AddForce(new Vector2(0, jumpHeight * 10));
        dontResetGroundedTime = dontResetGroundedTimer;
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        switch(other.gameObject.tag){
            case "Ground":
                OnLand();
                break;
            case "Obstacle":
                HurtPlayer();
                break;
        }
    }

    //Unity function that activates upon the player colliding with any Collider 2D
    private void OnLand(){
        if(isGroundedTimerTicking()){
            return;
        }
        isGrounded = true;
    }

    private void HurtPlayer(){

        if(isPlayerInvincible){
            return;
        }

        playerHealth -= 1;
        GameManager.instance.UpdateHealthUI(playerHealth);
        if(playerHealth <= 0){
            GameManager.instance.OnGameOver();
        }

        isPlayerInvincible = true;
        playerInvincibilityTime = playerInvincibilityFrameLength;
    }

    //Contains code that runs the grounded timer
    private void RunGroundedTimer(){
        if(isGroundedTimerTicking()){
            dontResetGroundedTime -= Time.deltaTime;
        }

        if(playerInvincibilityTime > 0){
            playerInvincibilityTime -= Time.deltaTime;

        } else if(isPlayerInvincible){
            isPlayerInvincible = false;
        }
    }

    //Boolean that checks to see if the grounded timer is above zero. This effectively controls a delay in which the player cannot be grounded
    private bool isGroundedTimerTicking(){
        return dontResetGroundedTime > 0;
    }
    
}
