using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource menuSource;
    public AudioClip selectSound;

    public void Start()
    {
        menuSource = GetComponent<AudioSource>();
    }
    public void Exit()
    {
        Application.Quit();
        menuSource.PlayOneShot(selectSound);
        print("The game is now over.");
    }

    public void StartButton()
    {
        menuSource.PlayOneShot(selectSound);
        SceneManager.LoadScene("FroggerExampleScene");
    }



}
    
