using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public int ObstaclesNumber;
    public List<float> zPositions;
  //  public List<Obstacle> obstacles;
    [SerializeField] public GameObject startRoad, endRoad, leftBorder, rightBorder;
    public float roadWidth;
    public float distanceBetweenObst;
    public int roadLines;
    public GameObject building;
    [SerializeField] ObjectPool[] pools;

    public int roadNumber;

    // Start is called before the first frame update
    void Awake()
    {
        zPositions = new List<float>();
      //  obstacles = new List<Obstacle>();
        InitializeObjects();
    }

    void InitializeObjects()
    {
        roadWidth = rightBorder.transform.position.x - leftBorder.transform.position.x;
        distanceBetweenObst = (endRoad.transform.position.z - startRoad.transform.position.z) / 3f;
    }
    public void CreateObstacles()
    {
        
        // zPositions = new List<float>();

        int r2 = Random.Range(0, ObstaclesNumber + 1);
        int counter = 0;
        while (counter <= r2)
        {
           // Debug.Log(i);
            //i++;

           
            float xPos = Random.Range(0, 2) == 0 ? -roadWidth / roadLines / 2 : roadWidth / roadLines / 2;
            float zPos = Random.Range(startRoad.transform.position.z, endRoad.transform.position.z);

            if (!CheckDistance(zPos, zPositions)) { counter++; continue; }
          //  Debug.Log(zPos);
            int r = Random.Range(0, pools.Length); // for objectpool
            GameObject obstacle = pools[r].GetPooledObject();
            obstacle.transform.parent = transform;
            obstacle.transform.position = new Vector3(xPos, obstacle.transform.position.y, zPos);
            obstacle.name = "Obstacle";
            counter++;
        }

    }
    bool CheckDistance(float zPos, List<float> zPositions)
    {
        bool goodPos = true;
        if (zPositions.Count > 0)
        {
            foreach (var item in zPositions)
            {
                if (Mathf.Abs(zPos - item) < distanceBetweenObst)
                {
                  //  Debug.Log(false);
                    goodPos = false;

                    break;
                }
            }
            zPositions.Add(zPos);
        }
        else zPositions.Add(zPos);
        return goodPos;
    }

    public void DeleteObstacles()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (var item in children)
        {
            if (item.name == "Obstacle")
            {
                item.GetComponent<Obstacle>().ObjectPool.ReturnToPool(item.gameObject);
            }
        }
        zPositions.Clear();
    }
    public void CreateBuildings()
    {
        GameObject newBuilding = Instantiate(building, transform.position,Quaternion.identity);
        newBuilding.transform.parent = transform;
    }
}
