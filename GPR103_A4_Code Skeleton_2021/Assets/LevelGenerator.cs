using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //variable to contain the bonus prefab
    public GameObject bonusPrefab;

    //spawn amount
    public int bonusCount = 2;

    //list of spawned bonuses
    public List<GameObject> spawnedBonus = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SpawnBonus()
    {
        
        for (int i = 0; i < bonusCount; i++)
        {   //here we've defined two random values, and then created a vector position out of those two values. 
            int randomXValue = Random.Range(-3, 3);
            Vector2 bonusPosition = new Vector2(randomXValue, -1.1f);

            GameObject tempObj = Instantiate(bonusPrefab, bonusPosition, Quaternion.identity) as GameObject;
            spawnedBonus.Add(tempObj);
        }
    }
}
