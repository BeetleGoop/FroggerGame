using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public AudioClip victorySound;
    private AudioSource soundSource; //audio container

    //special effects
    public GameObject explosionEffect; //particle effect for dying
    private GameManager myGameManager; //A reference to the GameManager in the scene.

    //ui 
    public float delayCounter = 1f; //a variable for the a delay counter, so that when you win or lose the screen doesnt change instantly


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
                KillPlayer(); //executes the kill player function
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) //a variety of collision enter checks
    {
        if(playerIsAlive == true) //checking if the player is alive
        {
            if (collision.transform.GetComponent<Vehicle>() != null) //if you hit a vehicle you die
            {
                KillPlayer();
            }
            else if(collision.transform.GetComponent<Platform>() != null) //if you hit a log you become its child, changes platform bool to true
            {
                transform.SetParent(collision.transform);
                isOnPlatform = true;
            }
            else if(collision.transform.tag == "Water") //if you hit the water, changes bool to true
            {
                isInWater = true;
            }
            else if(collision.transform.tag == "Bonus") //if collide with something tagged with bonus
            {
                myGameManager.CollectBonus(500, collision.transform.position);
                Destroy(collision.gameObject);
                soundSource.PlayOneShot(pickUpSound); //destroy the object, play a sound, and add 500 to the score.
            }
            else if (collision.transform.tag == "Victory") //if colliding with the victory tagged object you win!
            {
                Invoke("Victory", delayCounter); //invokes the new screen and also the delay
                soundSource.PlayOneShot(victorySound); //plays victory sound
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision) //on collision exit
    {
        if (playerIsAlive == true) //as long as the player is alive do the following
        {
            if(collision.transform.GetComponent<Platform>() != null)
            {
                transform.SetParent(null);
                isOnPlatform = false; //remove the player from the parent log, and update the log bool to false.
            }
            else if (collision.transform.tag == "Water")
            {
                isInWater = false; //update the water bool to false. 
            }
        }
    }

    void KillPlayer() //a custom function to kill the player
    {
        GetComponent<SpriteRenderer>().enabled = false; //turns off the sprite
        soundSource.PlayOneShot(deathSound); //plays a sound on death
        Instantiate(explosionEffect, transform.position, Quaternion.identity); //plays the explosion particle effect

        playerIsAlive = false; //changes the bool value of this variable
        playerCanMove = false;//changes the bool value of this variable
        print("there has been a terrible accident"); //prints to the console
        Invoke("GameOver", delayCounter); //executes the game over function, with a delay
    }

    public void GameOver() //a game over function to be called when the player dies. 
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Victory()//a victory function to be called when the goal is reached. 
    {
        SceneManager.LoadScene("Victory");
    }
}
