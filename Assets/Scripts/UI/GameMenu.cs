using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    //Function that loads the main menu scene, effectively quitting to the main menu
    public void QuitToMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    //Function that reloads the game scene, effectively restarting the game
    public void RestartGame(){
        SceneManager.LoadScene("MainGame");
    }
}
