using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //audio
    public AudioSource menuSource; //an audiosource for the menu
    public AudioClip selectSound; //a variable to drop the audio file into in the inspector

    public void Start()
    {
        menuSource = GetComponent<AudioSource>(); //starting by defining the menuSource as a gameobject
    }
    public void Exit() //a custom function that is executed by the menu buttons that quits the game
    {
        Application.Quit(); //quits the game
        menuSource.PlayOneShot(selectSound); //plays a sound when the button is pressed
        print("The game is now over."); //prints to the console to ensure the button is working during development
    }

    public void StartButton() //a custom function that is executed by the menu buttons that loads the main game scene.
    {
        menuSource.PlayOneShot(selectSound); //a sound is played when the button is pressed
        SceneManager.LoadScene("FroggerExampleScene"); //loads the main game scene
    }



}
    
