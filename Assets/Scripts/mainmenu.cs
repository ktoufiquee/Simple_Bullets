using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public void playGame(){

        SceneManager.LoadScene("Playground");
    }
    public void exitGame(){
        Debug.Log("Quit");
        Application.Quit(); 
    }
}
