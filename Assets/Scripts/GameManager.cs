using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    private bool isGameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public static GameManager instance;
    public int scoreGainRate = 1;
    public int scoreGain = 1;
    public GameObject gameOverUI;
    private float scoreTimer;

    // Awake is called ASAP
    void Awake()
    {
        scoreTimer = scoreGainRate;
        gameOverUI.SetActive(false);
        if(instance == null){
            instance = this;
        }


    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreTimer();
    }

    public void IncreaseScore(int increaseAmount){
        if(isGameOver){
            return;
        }
        
        score += increaseAmount;
        scoreText.text = "Score: " + score;
    }

    public void UpdateHealthUI(int playerHealth){
        healthText.text = "Health: " + playerHealth;
    }

    private void UpdateScoreTimer(){
        if(scoreTimer > 0){
            scoreTimer -= Time.deltaTime;
        } else {
            IncreaseScore(scoreGain);
            scoreTimer = scoreGainRate;
        }
    }

    public void OnGameOver(){
        isGameOver = true;
        gameOverUI.SetActive(true);
    }

    public bool GetGameOver(){
        return isGameOver;
    }
}
