using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] carPrefabs;
    public GameObject road;
    RoadSpawner roadSpawner;
    float spawnInterval = 2f;
    Vector3 spawnPos;
    [SerializeField] float spawnTimer;
    public bool canGenerate = true;

    void Start()
    {
        roadSpawner = GetComponent<RoadSpawner>();
        road = roadSpawner.roads[roadSpawner.roads.Count-2].gameObject;
        spawnPos = road.GetComponent<Road>().endRoad.transform.position;

        spawnTimer = spawnInterval;
        StartCoroutine( SpawnCar());
        Manager.Instance.stopStart += StopGenerate;
    }

    // Update is called once per frame
    IEnumerator SpawnCar()
    {
        while (canGenerate)
        {
            spawnTimer = spawnInterval + Random.Range(0, 2);
            int randomCarIndex = Random.Range(0, carPrefabs.Length); // Randomly select a car prefab
                                                                     // int randomLaneIndex = Random.Range(0, lanes.Length); // Randomly select a lane
            spawnPos = road.GetComponent<Road>().endRoad.transform.position;

            // Adjust the spawn position based on the lane width
            int direction = Random.Range(0, 2);
            spawnPos.x = direction == 0 ? -0.7f : 0.7f;

            GameObject newCar = Instantiate(carPrefabs[randomCarIndex], spawnPos, Quaternion.identity);
            Vector3 rotation = Vector3.zero;
            rotation.y = direction == 0 ? 0 : 180;
            newCar.transform.rotation = Quaternion.Euler(rotation);
            yield return new WaitForSeconds(spawnTimer);
        }
        
    }
    public void StopGenerate()
    {
        canGenerate = !canGenerate;
    }
        

}
