using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> roads;
    private float offset;
    ObstacleManager obstacleManager;

    // Start is called before the first frame update
    void Start()
    {
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList(); // to order roads by its z position
        }
        offset = roads[1].transform.position.z - roads[0].transform.position.z;
        obstacleManager = GetComponent<ObstacleManager>();
      
        for (int i = 0; i < roads.Count; i++)
        {
           // Debug.Log(i);
            obstacleManager.CreateObstacles(roads[i]);
        }
    }

    public void MoveRoad()
    {
        GameObject moveRoad = roads[0];
        roads.Remove(moveRoad);
        obstacleManager.DeleteObstacles(moveRoad);
        float newPosZ = roads[roads.Count - 1].transform.position.z + offset;
        moveRoad.transform.position = new Vector3(0, 0, newPosZ);
        roads.Add(moveRoad);
        obstacleManager.CreateObstacles(moveRoad);
        
    }
    
}
