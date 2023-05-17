using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField]List<GameObject> ObstaclesPrefabs;

    [SerializeField] GameObject[] Cars;
    [SerializeField] GameObject Fuel;
    List<GameObject> obstacles;
    [SerializeField] GameObject startRoad, endRoad, leftBorder, rightBorder;
    int ObstaclesOnRoad = 10;
    float roadWidth;
    public int roadLines;
    //public float offset;
    private void Awake()
    {
        obstacles = new List<GameObject>();
        
    }
    public void CreateObstacles(GameObject road)
    {

        startRoad = road.transform.Find("Start").gameObject;
        endRoad = road.transform.Find("End").gameObject;
        leftBorder = road.transform.Find("leftBorder").gameObject;
        rightBorder = road.transform.Find("rightBorder").gameObject;
        roadWidth = rightBorder.transform.localPosition.x - leftBorder.transform.localPosition.x;

        float distanceBetweenObst= (endRoad.transform.localPosition.y-startRoad.transform.localPosition.y)/ 15f;
        
        List<float> yPositions = new List<float>();
       
        int r2 = Random.Range(0, ObstaclesOnRoad+1);
        int counter = 0;
        while (counter <= r2)
        {
            int r = Random.Range(0, ObstaclesPrefabs.Count);
            GameObject obstacle = Instantiate(ObstaclesPrefabs[r]);
            obstacle.transform.parent = road.transform;

            float yPos = Random.Range(startRoad.transform.localPosition.y,endRoad.transform.localPosition.y);

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
            float xPos = Random.Range(0, 2) == 0 ? -roadWidth / 4 : roadWidth / 4;
            obstacle.transform.localPosition = new Vector3(xPos, yPos, obstacle.transform.localPosition.z);
           
            obstacle.name = "Obstacle";
            obstacles.Add(obstacle);
            counter++;
        }
    }

    public void DeleteObstacles(GameObject road)
    {
        Transform[] children = road.GetComponentsInChildren<Transform>();
        foreach (var item in children)
        {
            if (item.name == "Obstacle")
                Destroy(item.gameObject);

        }
    }
    public void CreateFuel()
    {

    }
    public void CreateCars()
    {

    }

}
