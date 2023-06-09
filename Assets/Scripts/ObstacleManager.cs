using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] GameObject[] Cars;
    [SerializeField] GameObject fuelPrefab;

    //GameObject startRoad, endRoad, leftBorder, rightBorder;
    [SerializeField] ObjectPool[] pools;
    GameObject fuel;
   
    int ObstaclesOnRoad = 5;
    float roadWidth;
    public int roadLines;
    public float fuelAppearDistance = 10;
    [SerializeField]GameObject Player;

    float distanceBetweenObst;
    bool createFuel = true;
    //public float offset;
    /*
    public void CreateObstacles(GameObject road)
    {
        InitializeObjects(road);
        List<float> yPositions = new List<float>();
        
        int r2 = Random.Range(0, ObstaclesOnRoad+1);
        int counter = 0;
        while (counter <= r2)
        {
            int r = Random.Range(0, pools.Length); // for objectpool
            GameObject obstacle = pools[r].GetPooledObject();
            obstacle.transform.parent = road.transform;

            float yPos = Random.Range(startRoad.transform.localPosition.y,endRoad.transform.localPosition.y);
            float xPos = Random.Range(0, 2) == 0 ? -roadWidth / roadLines / 2 : roadWidth / roadLines / 2;

              bool goodPos = true;
              if(yPositions.Count>0)
              {
                  foreach (var item in yPositions)
                  {
                      if(Mathf.Abs(yPos-item)<distanceBetweenObst)
                      {
                          goodPos = false;
                          break;
                      }
                  }
                  yPositions.Add(yPos);
              }
              if (!goodPos) continue;
            
            if (!CheckDistance(yPos, yPositions)) continue;
            
            obstacle.transform.localPosition = new Vector3(xPos, yPos, obstacle.transform.localPosition.z);
           
            obstacle.name = "Obstacle";
            counter++;
        }
        
    }*/
    private void Update()
    {

        if (Player.transform.position.z % fuelAppearDistance < 0.1f &&
            Player.transform.position.z % fuelAppearDistance > 0)
        {
            if (createFuel)
            {
                CreateFuel();
                createFuel = false;
            }
        }
        if (Player.transform.position.z % fuelAppearDistance > 0.1f &&
           Player.transform.position.z % fuelAppearDistance > 0)
        {
            createFuel = true;
        }
    }
    /*
    void InitializeObjects(GameObject road)
    {
        startRoad = road.transform.Find("Start").gameObject;
        endRoad = road.transform.Find("End").gameObject;
        leftBorder = road.transform.Find("leftBorder").gameObject;
        rightBorder = road.transform.Find("rightBorder").gameObject;
        roadWidth = rightBorder.transform.localPosition.x - leftBorder.transform.localPosition.x;
        distanceBetweenObst = (endRoad.transform.localPosition.y - startRoad.transform.localPosition.y) / 15f;
    }
    bool CheckDistance(float yPos, List<float> yPositions)
    {
        bool goodPos = true;
        if (yPositions.Count > 0)
        {
            foreach (var item in yPositions)
            {
                if (Mathf.Abs(yPos - item) < distanceBetweenObst)
                {
                    goodPos = false;
                    break;
                }
            }
            yPositions.Add(yPos);
        }
        return goodPos;
    }

    public void DeleteObstacles(GameObject road)
    {
        Transform[] children = road.GetComponentsInChildren<Transform>();
        foreach (var item in children)
        {
            if (item.name == "Obstacle")
            {
                item.GetComponent<Obstacle>().ObjectPool.ReturnToPool(item.gameObject);
            }
        }
    }*/
    public void CreateFuel()
    {
        float xPos = Random.Range(0, 2) == 0 ? -0.7f : 0.7f;
        if (fuel == null)
            fuel = Instantiate(fuelPrefab);
        fuel.transform.position = new Vector3(xPos, 0.5f,Player.transform.position.z +20f);
    }

}
