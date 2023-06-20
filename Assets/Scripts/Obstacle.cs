using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Obstacles
{
    Cone,
    Oil,
    Fence,
    Car,
    Fuel
}
public class Obstacle : MonoBehaviour
{
    public Obstacles obstacleType;
    public ObjectPool ObjectPool;
    public void CheckObstacle(PlayerMovement player)
    {
        if(Manager.Instance.Vibration) Handheld.Vibrate();
        switch (obstacleType)
        {
            case Obstacles.Cone:
                ConeAction(player);
                break;
            case Obstacles.Oil:
                OilAction(player);
                break;
            case Obstacles.Fence:
                Manager.Instance.GameOver();
                break;
            case Obstacles.Car:
                Manager.Instance.GameOver();
                break;
            case Obstacles.Fuel:
                FuelAction(player);
                break;

        }
    }    
    void ConeAction(PlayerMovement player)
    {
        player.Speed /= 2;
        Manager.Instance.obstacleNumber++;
    }

    void OilAction(PlayerMovement player)
    {
        GetComponent<SphereCollider>().isTrigger = false;
        GetComponent<SphereCollider>().enabled = false;
        int r = Random.Range(0, 2);
        if (r == 0)
        {
            if (!player.CheckForBorders(Vector3.right))
            {
                player.Move("MoveOilRight");
            }
            else if (!player.CheckForBorders(-Vector3.right))
                player.Move("MoveOilLeft");
        }
        else
        {
            if (!player.CheckForBorders(-Vector3.right))
                player.Move("MoveOilLeft");
            else if (!player.CheckForBorders(Vector3.right))
                player.Move("MoveOilRight");
        }
        Manager.Instance.obstacleNumber++;
    }
    void FuelAction(PlayerMovement player)
    {
        player.Refuel();
        Destroy(gameObject);
    }
   

}
