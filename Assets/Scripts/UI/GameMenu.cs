using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void QuitToMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame(){
        SceneManager.LoadScene("MainGame");
    }
}
