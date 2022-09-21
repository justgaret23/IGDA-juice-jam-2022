using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //The current score in-game
    private int score;

    //A boolean detecting if the game is over, which should be when the player's health reaches 0
    private bool isGameOver;

    //Variables for the score and health UI widgets that can be set in-editor
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    //A public instance of the GameManager that ANY other class can refer to.
    //This is very important for sharing functions.
    public static GameManager instance;

    //The interval at which the score counter updates (in seconds) that can be set in-editor
    //The float scoreTimer is what actually counts down, scoreGainRate just keeps data intact
    public int scoreGainRate = 1;
    private float scoreTimer;

    //The amount by which the score counter updates that can be set in-editor
    public int scoreGain = 1;

    //GameObject that contains the game over UI that can be set in-editor
    public GameObject gameOverUI;

    // Awake is called before the script is activated. We call Awake to:
        //set the initial timer
        //make the game over UI
        //Initialize the GameManager instance. 
            //This is why we use Awake instead of Start, so it will always go before the Player's Start and avoid an error where Player cannot find the GameMAnager
    private void Awake()
    {
        scoreTimer = scoreGainRate;
        gameOverUI.SetActive(false);
        if(instance == null){
            instance = this;
        }


    }

    // Update is called once per frame. All we really do here is update the score as the game goes on
    private void Update()
    {
        UpdateScoreTimer();
    }

    //Function that runs the timer updating the score at set intervals
    private void UpdateScoreTimer(){
        if(scoreTimer > 0){
            scoreTimer -= Time.deltaTime;
        } else {
            IncreaseScore(scoreGain);
            scoreTimer = scoreGainRate;
        }
    }

    //Function that increases the player's score and updates the UI widget accordingly. 
    //Called in UpdateScoreTimer()
    private void IncreaseScore(int increaseAmount){
        //If the game is over, do not increase the score.
        if(isGameOver){
            return;
        }

        score += increaseAmount;
        scoreText.text = "Score: " + score;
    }

    //NOTE: the following functions are public because we call them in the Player class through the instance. 
        //If they are not public, this is impossible to do.

    //Function that updates the health UI widget. Called in tandem with updating the player's health.
    public void UpdateHealthUI(int playerHealth){
        healthText.text = "Health: " + playerHealth;
    }


    //Function that runs upon the player losing all their health, aka  a game over. 
    //Sets the game over boolean to true and sets the game over UI to active.
    public void OnGameOver(){
        isGameOver = true;
        gameOverUI.SetActive(true);
    }
}
