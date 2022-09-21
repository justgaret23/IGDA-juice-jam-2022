using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Modifying this variable in editor lets you manipulate the player's jump height
    public float jumpHeight;

    //Modifying this variable in editor lets you manipulate the player's health
    public int playerHealth;

    //Rigidbody2D variable that holds the player's rigidbody, allowing us to manipulate it by adding forces
    private Rigidbody2D playerRigidbody;

    //Boolean that detects if the player is grounded
    private bool isGrounded;

    //Invincibility timer and boolean that handle player invincibility on hit
    public float playerInvincibilityFrameLength = 2f;
    private float playerInvincibilityTime = 0;
    private bool isPlayerInvincible = false;

    //Start is called before the first frame update. We use this to:
        //Properly initialize the playerRigidbody variable with the player's Rigidbody2D by getting it
        //Update the game's health UI by calling a function from the GameManager.
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        GameManager.instance.UpdateHealthUI(playerHealth);
    }

    //Update is called once per frame. This is used to:
        //run the player's invincibility timer
        //jump when the appropriate conditions are fulfilled.
    private void Update()
    {
        //The player can only jump when the following 2 conditions are fulfilled:
            //they press the jump button
            //isGrounded is true (which in normal cases is whenever the player is on the ground)
        if(Input.GetButtonDown("Jump") && isGrounded){
            Jump();
        }

        RunInvincibilityTimer();
    }

    //Function that contains logic for when the player jumps.
    //Does the following:
        //Apply a vertical force that can be changed in the editor 
        //Set isGrounded to false so the player can't infinijump
    private void Jump(){
        playerRigidbody.AddForce(new Vector2(0, jumpHeight * 10));
        isGrounded = false;
    }

    //Unity default function that activates whenever the player :
    //touches a Collider 2D that is marked as "Is Trigger" in the editor
    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.gameObject.tag){
            case "Obstacle":
                HurtPlayer();
                Destroy(other.gameObject);
                break;
        }
    }

    //Unity default function that activates whenever the player:
    //touches a Collider 2D that is NOT marked as "Is Trigger" in the editor
    private void OnCollisionEnter2D(Collision2D other) {
        switch(other.gameObject.tag){
            case "Ground":
                OnLand();
                break;
        }
    }

    //Unity function that activates upon the player colliding with any Collider 2D
    private void OnLand(){
        isGrounded = true;
    }

    //Function that contains logic for when the player is hit by an obstacle
    private void HurtPlayer(){
        //Check to see if player is invincible
        if(isPlayerInvincible){
            return;
        }

        //Decrease player health and update UI accordingly
        playerHealth -= 1;
        GameManager.instance.UpdateHealthUI(playerHealth);

        //If this hit is the fatal blow, run the game over code
        if(playerHealth <= 0){
            GameManager.instance.OnGameOver();
            Destroy(gameObject);
        }

        //Activate player invincibility post-hit
        isPlayerInvincible = true;
        playerInvincibilityTime = playerInvincibilityFrameLength;
    }

    //Contains code that runs the invincibility timer in the event the player gets hit
    private void RunInvincibilityTimer(){
        //If the timer is greater than zero, decrease the timer constantly. 
            //otherwise, remove the player's invincibility status if applicable.
        if(playerInvincibilityTime > 0){
            playerInvincibilityTime -= Time.deltaTime;

        } else if(isPlayerInvincible){
            isPlayerInvincible = false;
        }
    }
}
