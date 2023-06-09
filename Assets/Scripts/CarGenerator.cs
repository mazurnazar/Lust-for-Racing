using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] carPrefabs;
    float spawnInterval = 3f;
    Road road;
    Vector3 spawnPos;
    [SerializeField] float spawnTimer;
    public bool canGenerate = true;
    [SerializeField] GameObject player;

    void Start()
    {
        road = GetComponent<Road>();
        spawnPos = road.endRoad.transform.position;
        spawnTimer = spawnInterval;
        player = GameObject.Find("Player");
        if(GetComponent<Road>().roadNumber>1) SpawnCar();
    }

    // Update is called once per frame
    void Update()
    {
        if (canGenerate && DistanceToPlayer()) 
        {
            spawnTimer -= Time.deltaTime; // Decrease the spawn timer

            if (spawnTimer <= 0)
            {
                SpawnCar(); // Spawn a car
                spawnTimer = spawnInterval + Random.Range(0, 2); // Reset the spawn timer
            }
        }
    }
    void SpawnCar()
    {

        int randomCarIndex = Random.Range(0, carPrefabs.Length); // Randomly select a car prefab
                                                                 // int randomLaneIndex = Random.Range(0, lanes.Length); // Randomly select a lane
        spawnPos = road.endRoad.transform.position;

        // Adjust the spawn position based on the lane width
        spawnPos.x += Random.Range(0,2)==0?-0.7f:0.7f;
        Instantiate(carPrefabs[randomCarIndex], spawnPos, Quaternion.identity);
    }
    bool DistanceToPlayer()
    {
        if (GetComponent<Road>().endRoad.transform.position.z - player.transform.position.z < 5f) return false;
        else return true;
    }
}
