using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

/// <summary>
/// This script must be used as the core Player script for managing the player character in the game.
/// </summary>
public class Player : MonoBehaviour
{
    public string playerName = ""; //The players name for the purpose of storing the high score
   
    public int playerTotalLives; //Players total possible lives.
    public int playerLivesRemaining; //PLayers actual lives remaining.
   
    public bool playerIsAlive = true; //Is the player currently alive?
    public bool playerCanMove = false; //Can the player currently move?

    public bool isOnPlatform = false; //is the player on the log?
    public bool isInWater = false; //is the player in water?

    //Audio clips and container
    public AudioClip jumpSound; //variable for jumping sound

    public AudioClip deathSound; //variable for death sound

    public AudioClip pickUpSound; //variable for pickup sonund

    private AudioSource soundSource; //audio container

    //special effects
    public GameObject explosionEffect; //particle effect for dying

    private GameManager myGameManager; //A reference to the GameManager in the scene.


    // Start is called before the first frame update
    void Start() //on start, the game manager and the sound source is defined.
    {
        myGameManager = GameObject.FindObjectOfType<GameManager>();
        soundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsAlive == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < myGameManager.levelConstraintTop) //move up, but also dont allow upward movement if level constraint value exceeded.
            {
                transform.Translate(new Vector2(0, .75f));
                soundSource.PlayOneShot(jumpSound);
                myGameManager.UpdateScore(100);
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > myGameManager.levelConstraintBottom) //move down
            {
                transform.Translate(new Vector2(0, -.75f));
                soundSource.clip = jumpSound;
                soundSource.pitch = Random.Range(0.5f, 1f);
                soundSource.Play();
            }

            else if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > myGameManager.levelConstraintLeft) //move left
            {
                transform.Translate(new Vector2(-1, 0));
                soundSource.PlayOneShot(jumpSound);
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < myGameManager.levelConstraintRight) //move right
            {
                transform.Translate(new Vector2(1, 0));
                soundSource.PlayOneShot(jumpSound);
            }
        }
    }

    private void LateUpdate()
    {
        if(playerIsAlive == true) //kills the player if not on platform, but on water. 
        {
            if (isInWater == true && isOnPlatform == false) //this statement must be entirely true to kill the player.
            {
                KillPlayer();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerIsAlive == true)
        {
            if (collision.transform.GetComponent<Vehicle>() != null)
            {
                KillPlayer();
            }
            else if(collision.transform.GetComponent<Platform>() != null)
            {
                transform.SetParent(collision.transform);
                isOnPlatform = true;
            }
            else if(collision.transform.tag == "Water")
            {
                isInWater = true;
            }
            else if(collision.transform.tag == "Bonus")
            {
                myGameManager.CollectBonus(500, collision.transform.position);
                Destroy(collision.gameObject);
                soundSource.PlayOneShot(pickUpSound);
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerIsAlive == true)
        {
            if(collision.transform.GetComponent<Platform>() != null)
            {
                transform.SetParent(null);
                isOnPlatform = false;
            }
            else if (collision.transform.tag == "Water")
            {
                isInWater = false;
            }
        }
    }

    void KillPlayer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        soundSource.PlayOneShot(deathSound);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        playerIsAlive = false;
        playerCanMove = false;
        print("there has been a terrible accident");
    }

}
