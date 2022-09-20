using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float spawnTimer;
    public List<GameObject> spawnedPrefabs = new List<GameObject>();
    private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawnTimer();
    }

    private void UpdateSpawnTimer(){
        if(spawnTime <= 0){
            SpawnObstacle();
            spawnTime = spawnTimer;
        } else {
            spawnTime -= Time.deltaTime;
        }
    }

    private void SpawnObstacle(){
        int randomObstacle = Random.Range(0, spawnedPrefabs.Count);
        GameObject selectedObstacle = spawnedPrefabs[randomObstacle];
        Instantiate(selectedObstacle, transform.position, Quaternion.identity);
    }
}
