using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void CheckObstacle(Car car)
    {
        switch(obstacleType)
        {
            case Obstacles.Cone:
                ConeAction(car);
                break;
            case Obstacles.Oil:
                OilAction(car);
                break;
            case Obstacles.Fence:
                GameOver();
                break;
            case Obstacles.Car:
                GameOver();
                break;
            case Obstacles.Fuel:
                FuelAction(car);
                break;

        }
    }    
    void ConeAction(Car car)
    {
        car.Speed /= 2;
    }

    void OilAction(Car car)
    {
        int r = Random.Range(0, 1);
        if (r == 0)
        {
            if (!car.CheckForBorders(Vector3.right))
                car.MoveDirection("right");
            else if (!car.CheckForBorders(-Vector3.right))
                car.MoveDirection("left");
        }
        else
        {
            if (!car.CheckForBorders(-Vector3.right))
                car.MoveDirection("left");
            else if (!car.CheckForBorders(Vector3.right))
                car.MoveDirection("right");
        }
    }
    void GameOver()
    {
        Debug.Log("gameover");
    }
    void FuelAction(Car car)
    {
        car.Refuel();
    }

}
