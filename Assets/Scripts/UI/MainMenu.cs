using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Function that loads the MainGame scene, effectively starting the game
    public void StartGame(){
        SceneManager.LoadScene("MainGame");
    }

    //Function that runs application.Quit, effectively quitting the game.
        //NOTE: this only works in a build of the game. Running this function in-editor will not do anything.
    public void QuitGame(){
        Debug.Log("The game was quit!");
        Application.Quit();
    }
}
