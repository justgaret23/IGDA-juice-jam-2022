using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //Movement speed of the obstacle
    public float moveSpeed;

    //Rigidbody2D variable that holds the obstacle rigidbody
    private Rigidbody2D obstacleRigidbody;

    //enumerator that determines the spawn location of obstacles
    public enum SPAWNSTATE {
        Low,
        Center,
        High
    }

    //local enumerator to determine spawn location
    public SPAWNSTATE spawnState;


    // Start is called before the first frame update
    void Start()
    {
        obstacleRigidbody = GetComponent<Rigidbody2D>();
        obstacleRigidbody.velocity = new Vector2(-moveSpeed, 0);
        FixSpawnPosition(spawnState);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForDespawn();
    }

    private void FixSpawnPosition(SPAWNSTATE spawnState){
        switch(spawnState){
            case SPAWNSTATE.Low:
                transform.position = new Vector2(transform.position.x, -2);
                break;
            case SPAWNSTATE.Center:
                transform.position = new Vector2(transform.position.x, -1);
                break;
            case SPAWNSTATE.High:
                transform.position = new Vector2(transform.position.x, 0);
                break;

        }
    }

    private void CheckForDespawn(){
        if(transform.position.x < -25){
            Destroy(this.gameObject);
        }
    }
}
