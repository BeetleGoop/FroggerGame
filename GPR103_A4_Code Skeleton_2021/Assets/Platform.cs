using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int moveDirection = 0; //This variabe is to be used to indicate the direction the vehicle is moving in.
    public float speed; //This variable is to be used to control the speed of the vehicle.
    public Vector2 startingPosition; //This variable is to be used to indicate where on the map the vehicle starts (or spawns)
    public Vector2 endPosition; //This variablle is to be used to indicate the final destination of the vehicle.

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPosition; //defining start position
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed * moveDirection);//this moves the log

        if ((transform.position.x * moveDirection) < (endPosition.x * moveDirection))
        {
            transform.position = startingPosition; //loops the movement of the logs
        }
    }
}
