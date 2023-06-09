using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private List<Road> roads;
    [SerializeField] private List<GameObject> buildings;

    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList(); // to order roads by its z position
        }
        offset = roads[1].transform.position.z - roads[0].transform.position.z;
        for (int i = 0; i < roads.Count; i++)
        {
            if(roads[i].roadNumber >1)
            roads[i].CreateObstacles();
            roads[i].roadNumber=i;
            roads[i].building = SelectBuildingToGenerate();
            roads[i].CreateBuildings();
        }


    }
    public void MoveRoad()
    {
        Road moveRoad = roads[0];
        roads.Remove(moveRoad);
        moveRoad.DeleteObstacles();
        //obstacleManager.DeleteObstacles(moveRoad);
        float newPosZ = roads[roads.Count - 1].transform.position.z + offset;
        moveRoad.transform.position = new Vector3(0, 0, newPosZ);
        roads.Add(moveRoad);
        moveRoad.CreateObstacles();
        moveRoad.building = SelectBuildingToGenerate();
        moveRoad.CreateBuildings();
        moveRoad.GetComponent<CarGenerator>().canGenerate = true;
        // obstacleManager.CreateObstacles(moveRoad);
    }
    GameObject SelectBuildingToGenerate()
    {
        int buildingNumber = Random.Range(0, buildings.Count );

        return buildings[buildingNumber];
    }
}
