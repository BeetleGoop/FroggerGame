using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script is to be attached to a GameObject called GameManager in the scene. It is to be used to manager the settings and overarching gameplay loop.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Scoring")]
    public int currentScore = 0; //The current score in this round.
    public int highScore = 0; //The highest score achieved either in this session or over the lifetime of the game.
    public TMP_Text currentScoreUI; //text object for the current score
    public TMP_Text highScoreText; //text object for the high score

    [Header("Playable Area")]
    public float levelConstraintTop; //The maximum positive Y value of the playable space.
    public float levelConstraintBottom; //The maximum negative Y value of the playable space.
    public float levelConstraintLeft; //The maximum negative X value of the playable space.
    public float levelConstraintRight; //The maximum positive X value of the playablle space.

    [Header("Gameplay Loop")]
    public bool isGameRunning; //Is the gameplay part of the game current active?
    public float totalGameTime; //The maximum amount of time or the total time avilable to the player.
    public float gameTimeRemaining; //The current elapsed time

    [Header("FX")]
    public GameObject bonusCollectEffect; //the particle effect for the bonus object

    [Header("Bonus Respawn")]
    public GameObject bonusPrefab; //variable to contain the bonus prefab
    public int bonusCount = 1; //spawn amount
    public List<GameObject> spawnedBonus = new List<GameObject>(); //list of spawned bonuses

    // Start is called before the first frame update
    void Start()
    {
        //gets the previous value of the high score text and prints it to a string so it can be displayed in the UI, if there is no value it uses the default 0
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        UpdateScore(-currentScore);
        currentScoreUI.text = "0";

        int randomCarXValue = Random.Range(-3, 3); //spawns a bonus pickup in the car area
        Vector2 bonusCarPosition = new Vector2(randomCarXValue, -1.1f);
        GameObject tempCarObj = Instantiate(bonusPrefab, bonusCarPosition, Quaternion.identity) as GameObject;
        spawnedBonus.Add(tempCarObj);
        
        int randomLogXValue = Random.Range(-3, 3); //spawns a bonus pickup in the log area
        Vector2 bonusLogPosition = new Vector2(randomLogXValue, 1.7f);
        GameObject tempLogObj = Instantiate(bonusPrefab, bonusLogPosition, Quaternion.identity) as GameObject;
        spawnedBonus.Add(tempLogObj);
        
    }

    // Update is called once per frame
    void Update()
    {
        //https://www.youtube.com/watch?v=vZU51tbgMXk high score system tutorial from here.
        PlayerPrefs.SetInt("HighScore", currentScore); //gets the value of the current score and sets it as the high score.
    }

    public void UpdateScore(int scoreAmount) //takes the current score and converts it to a string for the ui
    {
        currentScore += scoreAmount;
        currentScoreUI.text = currentScore.ToString();

    }

    public void CollectBonus(int amount, Vector2 pos ) //triggers the collect bonus function, with a nice particle effect. 
    {
        UpdateScore(amount);
        Instantiate(bonusCollectEffect, pos, Quaternion.identity);

    }
}
