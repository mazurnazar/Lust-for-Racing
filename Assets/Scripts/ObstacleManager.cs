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
   
    public float fuelAppearDistance = 10;
    [SerializeField] GameObject Player;

    bool createFuel = true;
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
    public void CreateFuel()
    {
        float xPos = Random.Range(0, 2) == 0 ? -0.7f : 0.7f;
        if (fuel == null)
            fuel = Instantiate(fuelPrefab);
        fuel.transform.position = new Vector3(xPos, 0.5f,Player.transform.position.z +20f);
    }

}
